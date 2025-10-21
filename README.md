# SGI - Sistema de Gestão de Igreja


![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-lightblue)
![MySQL](https://img.shields.io/badge/Database-MySQL-orange)
![License](https://img.shields.io/badge/License-Proprietário-red)

## 📋 Sobre o Projeto

O **SGI (Sistema de Gestão de Igreja)** é uma aplicação desktop desenvolvida em C# com Windows Forms, projetada para auxiliar na administração e gestão de atividades eclesiásticas. O sistema oferece uma interface moderna e intuitiva para o gerenciamento completo das operações de uma igreja.

<div align="center">
  <img src="b.png" alt="SGI Sistema" width="600"/>
</div>

## 🚀 Funcionalidades Principais

### 🔐 Sistema de Autenticação e Segurança
- **Login Seguro**: Autenticação com criptografia de senhas
- **Controle de Sessão**: Gerenciamento de usuários logados
- **Recuperação de Senha**: Sistema de recuperação integrado
- **Configuração de Conexão**: Atalho especial (Ctrl+Shift+Alt+B) para configurar banco

### 🎨 Interface e Controles Customizados
- **ECTurbo_Botao**: Botões personalizados com design moderno
- **ECTurbo_TextBox**: Campos de texto com validação automática
- **ECTurbo_ComboBox**: Listas suspensas estilizadas
- **ECTurbo_CheckBox/RadioButton**: Controles de seleção customizados
- **ECTurbo_Grafico1-5**: Cinco tipos diferentes de gráficos para visualização
- **ECTurbo_MenuLateral**: Menu lateral responsivo
- **ECTurbo_BarraProgresso**: Indicadores de progresso visuais
- **ECTurbo_Datas/Anos**: Controles especializados para datas
- **ECTurbo_Numericos**: Campos numéricos com formatação
- **ECTurbo_MaskedTextBox**: Campos com máscara de entrada
- **ECTurbo_Panel**: Painéis com bordas arredondadas
- **ECTurbo_Separadores**: Separadores horizontais e verticais

### 🌐 Integração com APIs Externas
- **Busca de CEP**: Consulta automática via ViaCEP
- **Consulta de CNPJ**: Validação e busca de dados empresariais via BrasilAPI
- **Requisições HTTP**: Sistema genérico para consumo de APIs

### 🗄️ Gerenciamento de Dados
- **MySQL Avançado**: Camada completa de acesso a dados
- **Parâmetros Dinâmicos**: Sistema flexível de consultas parametrizadas
- **CRUD Completo**: Operações de Create, Read, Update, Delete
- **Busca Múltipla**: Pesquisa em múltiplos campos simultaneamente
- **Grid Dinâmico**: Sistema de grid com paginação e formatação

### 📊 Sistema de Relatórios
- **ReportViewer**: Geração de relatórios integrada
- **Exportação**: Múltiplos formatos de saída
- **Visualização**: Interface moderna para relatórios

### 🛠️ Utilitários e Funcionalidades Auxiliares
- **Manipulação de Imagens**: Upload, redimensionamento e miniaturas
- **Seleção de Arquivos**: Interface para seleção única ou múltipla
- **Envio de Email**: Sistema completo de envio com anexos
- **Validações**: Sistema robusto de validação de dados
- **Mensagens**: Sistema unificado de alertas, erros e confirmações
- **Criptografia**: Funções de segurança e geração de chaves
- **Formatação**: Formatação automática de números, datas e textos
- **Estados/UF**: Dicionário completo de estados brasileiros

### 🎯 Recursos Especiais
- **Modal Customizado**: Sistema de janelas modais com opacidade
- **Limpeza Automática**: Limpeza inteligente de formulários
- **Tags Configuráveis**: Sistema de configuração via tags nos controles
- **Cores Dinâmicas**: Manipulação e transparência de cores
- **Paths Gráficos**: Criação de formas com bordas arredondadas

## 🛠️ Tecnologias Utilizadas

### Framework e Linguagem
- **.NET 8.0** - Framework principal
- **C#** - Linguagem de programação
- **Windows Forms** - Interface gráfica

### Banco de Dados
- **MySQL 9.3.0** - Sistema de gerenciamento de banco de dados
- **MySql.Data** - Conector oficial MySQL para .NET

### Bibliotecas e Dependências
- **ReportViewerCore.WinForms 15.1.26** - Geração de relatórios
- **Newtonsoft.Json 13.0.3** - Manipulação de JSON
- **BouncyCastle.Cryptography 2.6.1** - Criptografia avançada
- **Google.Protobuf 3.31.1** - Serialização de dados
- **ZstdSharp.Port 0.8.5** - Compressão de dados

## 📁 Estrutura do Projeto

```
SGI_Igreja/
├── 📁 Codigos/              # Classes de negócio e utilitários
│   ├── APIs.cs              # Sistema genérico para consumo de APIs
│   ├── BuscaCEP.cs          # Consulta automática de CEP via ViaCEP
│   ├── BuscaCNPJ.cs         # Validação e consulta CNPJ via BrasilAPI
│   ├── Config.cs            # Configurações gerais do sistema
│   ├── Funcoes.cs           # 1300+ linhas de funções utilitárias
│   ├── GRID.cs              # Sistema avançado de grids dinâmicos
│   └── MYSQL.cs             # Camada completa de acesso a dados MySQL
├── 📁 Controles/            # 23 Controles customizados ECTurbo
│   ├── ECTurbo_Botao.cs     # Botões com design moderno
│   ├── ECTurbo_TextBox.cs   # Campos de texto com validação
│   ├── ECTurbo_ComboBox.cs  # ComboBox estilizado
│   ├── ECTurbo_CheckBox.cs  # Checkbox personalizado
│   ├── ECTurbo_Grafico1-5.cs# Cinco tipos de gráficos
│   ├── ECTurbo_MenuLateral.cs# Menu lateral responsivo
│   ├── ECTurbo_BarraProgresso.cs# Indicadores de progresso
│   ├── ECTurbo_Datas.cs     # Controles especializados para datas
│   ├── ECTurbo_Numericos.cs # Campos numéricos formatados
│   ├── ECTurbo_Panel.cs     # Painéis com bordas arredondadas
│   └── [outros 13 controles]# Diversos controles especializados
├── 📁 Formularios/          # Interface do usuário
│   ├── FrmLogin.cs          # Tela de autenticação
│   ├── FrmPrincipal.cs      # Interface principal do sistema
│   ├── FrmConexaoMySQL.cs   # Configuração de banco de dados
│   ├── FormMsg.cs           # Sistema de mensagens unificado
│   └── FrmGRID_Modelo.cs    # Template para grids
├── 📁 LinhasModeloGRID/     # Modelos de linha para grids
│   └── Lst_Modelo.cs        # Template base para listagens
├── 📁 Resources/            # Recursos visuais
│   ├── logo.jpg             # Logo do sistema
│   ├── pomba.png            # Ícone religioso
│   ├── icons8-*.png         # Ícones da interface
│   └── [outros recursos]    # Imagens e ícones diversos
├── 📁 Properties/           # Configurações do projeto
└── Program.cs               # Ponto de entrada da aplicação
```

## ⚙️ Pré-requisitos

### Sistema Operacional
- **Windows 10** ou superior
- **.NET 8.0 Runtime** instalado

### Banco de Dados
- **MySQL Server 8.0** ou superior
- **MySQL Workbench** (recomendado para administração)

### Desenvolvimento
- **Visual Studio 2022** (versão 17.10 ou superior)
- **MySQL Connector/NET**

## 🔧 Instalação e Configuração

### 1. Clone o Repositório
```bash
git clone [URL_DO_REPOSITORIO]
cd SGI-Igreja
```

### 2. Configuração do Banco de Dados
1. Instale o MySQL Server
2. Crie um banco de dados para o sistema
3. Execute os scripts de criação das tabelas (se disponíveis)

### 3. Configuração da Aplicação
1. Abra o projeto no Visual Studio
2. Restaure os pacotes NuGet:
   ```
   dotnet restore
   ```
3. Configure a string de conexão no primeiro acesso da aplicação

### 4. Compilação
```bash
dotnet build --configuration Release
```

## 🚀 Como Executar

### Desenvolvimento
1. Abra o projeto no Visual Studio
2. Pressione `F5` ou clique em "Iniciar Depuração"

### Produção
1. Navegue até a pasta `bin/Release/net8.0-windows/`
2. Execute o arquivo `SGI_Igreja.exe`

## 🔐 Primeiro Acesso

1. **Configuração de Conexão**: Na primeira execução, configure a conexão com o banco MySQL
2. **Login**: Use as credenciais padrão do administrador (consulte a documentação interna)
3. **Configuração Inicial**: Complete a configuração inicial do sistema

### Atalhos Especiais
- **Ctrl+Shift+Alt+B**: Abre a configuração de conexão do banco de dados

## 📊 Funcionalidades Detalhadas

### Sistema de Autenticação
- Login com usuário e senha
- Criptografia de senhas com algoritmos seguros
- Controle de sessão de usuário
- Recuperação de senha

### Controles Personalizados
- **ECTurbo_Botao**: Botões com design moderno
- **ECTurbo_TextBox**: Campos de texto com validação
- **ECTurbo_ComboBox**: Listas suspensas customizadas
- **ECTurbo_CheckBox**: Caixas de seleção estilizadas
- **ECTurbo_Grafico**: Componentes para visualização de dados

### Integração com APIs
- **Busca de CEP**: Integração com serviços de consulta de endereço
- **Consulta de CNPJ**: Validação e busca de dados empresariais

## 🔧 Configurações Avançadas

### Arquivo de Configuração (App.config)
```xml
<userSettings>
    <SGI_Igreja.Properties.Settings>
        <setting name="MySQL_BD" serializeAs="String">
            <value>[NOME_DO_BANCO]</value>
        </setting>
        <setting name="MySQL_Servidor" serializeAs="String">
            <value>[SERVIDOR]</value>
        </setting>
        <setting name="MySQL_Porta" serializeAs="String">
            <value>[PORTA]</value>
        </setting>
    </SGI_Igreja.Properties.Settings>
</userSettings>
```

## 🐛 Solução de Problemas

### Problemas Comuns

**Erro de Conexão com MySQL**
- Verifique se o MySQL Server está executando
- Confirme as credenciais de acesso
- Teste a conectividade de rede

**Erro de Dependências**
- Execute `dotnet restore` para restaurar pacotes
- Verifique se o .NET 8.0 está instalado

**Problemas de Performance**
- Otimize as consultas SQL
- Verifique índices no banco de dados
- Monitore o uso de memória

## 📈 Roadmap

- [ ] Migração para .NET 9.0
- [ ] Interface web complementar
- [ ] API REST para integração
- [ ] Módulo de relatórios avançados
- [ ] Backup automático
- [ ] Sincronização em nuvem

## 🤝 Contribuição

Para contribuir com o projeto:

1. Faça um fork do repositório
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto é proprietário. Todos os direitos reservados.

## 📞 Suporte

Para suporte técnico ou dúvidas sobre o sistema:

- **Email**: [bartolomeugasparbg@gmail.com]
- **Telefone**: [921389141]
- **Documentação**: [link-para-documentacao]

## 📋 Changelog

### Versão Atual
- Sistema de login implementado
- Controles customizados funcionais
- Integração com MySQL estável
- Sistema de relatórios básico

---

**Desenvolvido com ❤️ para a gestão eclesiástica moderna**
