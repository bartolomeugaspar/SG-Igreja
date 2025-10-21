#!/bin/bash

echo "🚀 SGI Igreja - Script de Configuração de Desenvolvimento"
echo "========================================================="

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Função para verificar espaço em disco
check_disk_space() {
    echo -e "${BLUE}📊 Verificando espaço em disco...${NC}"
    df -h / | tail -1 | awk '{print "Uso: " $5 " | Disponível: " $4}'
    
    USAGE=$(df / | tail -1 | awk '{print $5}' | sed 's/%//')
    if [ $USAGE -gt 90 ]; then
        echo -e "${RED}⚠️  Aviso: Disco com mais de 90% de uso!${NC}"
        echo -e "${YELLOW}💡 Execute: sudo apt clean && sudo apt autoremove${NC}"
        return 1
    fi
    return 0
}

# Função para verificar .NET
check_dotnet() {
    echo -e "${BLUE}🔍 Verificando .NET SDK...${NC}"
    if command -v dotnet &> /dev/null; then
        VERSION=$(dotnet --version)
        echo -e "${GREEN}✅ .NET SDK $VERSION instalado${NC}"
        return 0
    else
        echo -e "${RED}❌ .NET SDK não encontrado${NC}"
        return 1
    fi
}

# Função para verificar VS Code
check_vscode() {
    echo -e "${BLUE}🔍 Verificando VS Code...${NC}"
    if command -v code &> /dev/null; then
        echo -e "${GREEN}✅ VS Code instalado${NC}"
        return 0
    else
        echo -e "${YELLOW}⚠️  VS Code não encontrado no PATH${NC}"
        return 1
    fi
}

# Função para instalar extensões do VS Code
install_extensions() {
    echo -e "${BLUE}📦 Instalando extensões do VS Code...${NC}"
    
    extensions=(
        "ms-dotnettools.csharp"
        "ms-dotnettools.vscode-dotnet-runtime"
        "formulahendry.dotnet-test-explorer"
        "jchannon.csharpextensions"
    )
    
    for ext in "${extensions[@]}"; do
        echo -e "${YELLOW}Instalando: $ext${NC}"
        code --install-extension "$ext" --force
    done
}

# Função para limpar projeto
clean_project() {
    echo -e "${BLUE}🧹 Limpando projeto...${NC}"
    cd SGI_Igreja
    dotnet clean
    rm -rf bin obj
    cd ..
}

# Função para restaurar dependências
restore_packages() {
    echo -e "${BLUE}📦 Restaurando pacotes NuGet...${NC}"
    cd SGI_Igreja
    
    # Tentar restaurar com timeout
    timeout 60s dotnet restore
    RESULT=$?
    
    if [ $RESULT -eq 0 ]; then
        echo -e "${GREEN}✅ Pacotes restaurados com sucesso${NC}"
    elif [ $RESULT -eq 124 ]; then
        echo -e "${YELLOW}⏰ Timeout na restauração (pode ser problema de rede)${NC}"
    else
        echo -e "${RED}❌ Erro na restauração de pacotes${NC}"
    fi
    
    cd ..
    return $RESULT
}

# Função para compilar projeto
build_project() {
    echo -e "${BLUE}🔨 Compilando projeto...${NC}"
    cd SGI_Igreja
    
    dotnet build --no-restore
    RESULT=$?
    
    if [ $RESULT -eq 0 ]; then
        echo -e "${GREEN}✅ Projeto compilado com sucesso${NC}"
    else
        echo -e "${RED}❌ Erro na compilação${NC}"
    fi
    
    cd ..
    return $RESULT
}

# Função principal
main() {
    echo -e "${BLUE}🎯 Iniciando configuração...${NC}"
    
    # Verificações básicas
    check_disk_space
    DISK_OK=$?
    
    check_dotnet
    DOTNET_OK=$?
    
    check_vscode
    VSCODE_OK=$?
    
    # Relatório de status
    echo ""
    echo -e "${BLUE}📋 Status do Ambiente:${NC}"
    echo "===================="
    
    if [ $DISK_OK -eq 0 ]; then
        echo -e "💾 Espaço em Disco: ${GREEN}OK${NC}"
    else
        echo -e "💾 Espaço em Disco: ${RED}PROBLEMA${NC}"
    fi
    
    if [ $DOTNET_OK -eq 0 ]; then
        echo -e "🔧 .NET SDK: ${GREEN}OK${NC}"
    else
        echo -e "🔧 .NET SDK: ${RED}NÃO INSTALADO${NC}"
    fi
    
    if [ $VSCODE_OK -eq 0 ]; then
        echo -e "💻 VS Code: ${GREEN}OK${NC}"
    else
        echo -e "💻 VS Code: ${YELLOW}NÃO ENCONTRADO${NC}"
    fi
    
    echo ""
    
    # Ações baseadas no status
    if [ $DOTNET_OK -eq 0 ] && [ $DISK_OK -eq 0 ]; then
        echo -e "${GREEN}🚀 Ambiente pronto para desenvolvimento!${NC}"
        
        if [ $VSCODE_OK -eq 0 ]; then
            echo -e "${YELLOW}Deseja instalar extensões do VS Code? (y/n)${NC}"
            read -r response
            if [[ "$response" =~ ^([yY][eE][sS]|[yY])$ ]]; then
                install_extensions
            fi
        fi
        
        echo -e "${YELLOW}Deseja limpar e restaurar o projeto? (y/n)${NC}"
        read -r response
        if [[ "$response" =~ ^([yY][eE][sS]|[yY])$ ]]; then
            clean_project
            restore_packages
            build_project
        fi
        
    else
        echo -e "${RED}❌ Ambiente não está pronto${NC}"
        echo -e "${YELLOW}💡 Resolva os problemas acima antes de continuar${NC}"
    fi
    
    echo ""
    echo -e "${BLUE}📚 Comandos Úteis:${NC}"
    echo "=================="
    echo "• dotnet restore          - Restaurar pacotes"
    echo "• dotnet build           - Compilar projeto"
    echo "• dotnet clean           - Limpar projeto"
    echo "• code .                 - Abrir no VS Code"
    echo "• ./dev-setup.sh         - Executar este script"
    echo ""
    echo -e "${GREEN}✨ Configuração concluída!${NC}"
}

# Executar função principal
main
