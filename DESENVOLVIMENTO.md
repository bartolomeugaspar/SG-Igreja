# 🛠️ Guia de Desenvolvimento - SGI Igreja no VS Code

## 🚀 Configuração Inicial

### 1. **Executar Script de Configuração**
```bash
./dev-setup.sh
```

### 2. **Abrir no VS Code**
```bash
code .
```

## 📋 **Comandos Disponíveis no VS Code**

### **Via Command Palette (Ctrl+Shift+P)**
- `Tasks: Run Task` → Escolher tarefa
- `C#: Restart OmniSharp` → Reiniciar IntelliSense
- `.NET: Generate Assets for Build and Debug`

### **Via Terminal Integrado (Ctrl+`)**
```bash
# Restaurar dependências
dotnet restore

# Compilar projeto
dotnet build

# Limpar projeto
dotnet clean

# Publicar projeto
dotnet publish -c Release
```

### **Atalhos de Teclado**
- `Ctrl+Shift+B` → Build (compilar)
- `F5` → Debug (não funcionará no Linux)
- `Ctrl+Shift+P` → Command Palette
- `Ctrl+Shift+E` → Explorer
- `Ctrl+Shift+G` → Git

## 🔧 **Estrutura de Desenvolvimento**

### **Arquivos de Configuração Criados**
```
.vscode/
├── tasks.json          # Tarefas de build/clean/restore
├── launch.json         # Configuração de debug
├── settings.json       # Configurações do workspace
└── extensions.json     # Extensões recomendadas
```

### **Tarefas Disponíveis**
1. **Build** - Compilar o projeto
2. **Clean** - Limpar arquivos temporários
3. **Restore** - Restaurar pacotes NuGet
4. **Publish** - Publicar aplicação
5. **Watch** - Compilação automática

## 🎯 **Fluxo de Desenvolvimento Recomendado**

### **1. Edição de Código**
- Use o **IntelliSense** para autocompletar
- **Ctrl+.** para ações rápidas (Quick Actions)
- **F12** para ir à definição
- **Shift+F12** para encontrar referências

### **2. Compilação**
```bash
# Via terminal
dotnet build

# Via VS Code
Ctrl+Shift+B
```

### **3. Verificação de Erros**
- Painel **Problems** (Ctrl+Shift+M)
- Sublinhados vermelhos no código
- Output do OmniSharp

### **4. Controle de Versão**
```bash
# Adicionar mudanças
git add .

# Commit
git commit -m "Descrição das mudanças"

# Push
git push origin main
```

## 🐛 **Debugging (Limitado no Linux)**

### **Alternativas para Debug**
1. **Logging Extensivo**
   ```csharp
   Console.WriteLine($"Debug: {variavel}");
   System.Diagnostics.Debug.WriteLine("Debug info");
   ```

2. **Breakpoints Condicionais**
   ```csharp
   #if DEBUG
   // Código de debug
   #endif
   ```

3. **Unit Tests**
   ```csharp
   [Test]
   public void TesteMetodo()
   {
       // Teste do método
   }
   ```

## 📦 **Gerenciamento de Dependências**

### **Adicionar Pacote NuGet**
```bash
dotnet add package NomeDoPacote
```

### **Remover Pacote**
```bash
dotnet remove package NomeDoPacote
```

### **Listar Pacotes**
```bash
dotnet list package
```

## 🔍 **Análise de Código**

### **Extensões Úteis Instaladas**
- **C# for Visual Studio Code** - IntelliSense e debugging
- **C# Extensions** - Snippets e templates
- **.NET Core Test Explorer** - Execução de testes
- **XML Tools** - Para arquivos .csproj

### **Configurações de Qualidade**
- **Format on Save** habilitado
- **Organize Imports** automático
- **Roslyn Analyzers** ativo

## 🚨 **Limitações Conhecidas**

### **❌ Não Funciona no Linux**
- Execução da aplicação (Windows Forms)
- Debug visual da interface
- Preview de formulários

### **✅ Funciona Perfeitamente**
- Edição de código com IntelliSense
- Compilação e verificação de erros
- Refatoração de código
- Controle de versão
- Análise estática

## 💡 **Dicas de Produtividade**

### **Snippets Úteis**
- `cw` → `Console.WriteLine()`
- `ctor` → Constructor
- `prop` → Property
- `class` → Class template

### **Navegação Rápida**
- `Ctrl+P` → Ir para arquivo
- `Ctrl+Shift+O` → Ir para símbolo
- `Ctrl+G` → Ir para linha
- `Alt+Left/Right` → Navegar histórico

### **Refatoração**
- `F2` → Renomear símbolo
- `Ctrl+.` → Quick fixes
- `Shift+Alt+F` → Formatar documento

## 🔄 **Workflow Híbrido (Recomendado)**

### **Desenvolvimento no Linux (VS Code)**
1. Editar código
2. Compilar e verificar erros
3. Commit das mudanças
4. Push para repositório

### **Teste no Windows**
1. Pull das mudanças
2. Executar aplicação
3. Testar funcionalidades
4. Reportar bugs/melhorias

## 📞 **Solução de Problemas**

### **OmniSharp não funciona**
```bash
# Reiniciar OmniSharp
Ctrl+Shift+P → "C#: Restart OmniSharp"
```

### **IntelliSense lento**
```bash
# Limpar cache
rm -rf ~/.omnisharp
```

### **Problemas de compilação**
```bash
# Limpar e restaurar
dotnet clean
dotnet restore
dotnet build
```

### **Espaço em disco**
```bash
# Limpar sistema
sudo apt clean
sudo apt autoremove
```

## 🎯 **Próximos Passos**

1. **Configurar Git** se ainda não estiver configurado
2. **Instalar extensões adicionais** conforme necessidade
3. **Criar branch de desenvolvimento** para mudanças
4. **Configurar CI/CD** para builds automáticos
5. **Documentar APIs** e métodos principais

---

**💻 Desenvolvido para maximizar a produtividade no VS Code, mesmo com limitações de plataforma!**
