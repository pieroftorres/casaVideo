using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace minhaAutomacao
{
    [TestClass]
    public class ST001_CadastroDoCliente
    {
        public static string chromeWebDriver = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        // URL da página a ser testada
        public static string linkSiteProjeto = "http://www.casaevideo.com.br/";

        [TestMethod]
        public void CT001_EfetuarLoginComSucesso()
        {

            string urlPaginaAtual = "";
            string urlPaginaLogin = "https://listapresente.casaevideo.com.br/login?utm_source=site&utm_medium=header";
            string urlPaginaCadastro = "https://listapresente.casaevideo.com.br/cadastro";
            string urlCadastroCompleto = "https://listapresente.casaevideo.com.br/";

            IWebDriver driver = new ChromeDriver(chromeWebDriver);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(linkSiteProjeto);

            IWebElement entreOuCadastre = driver.FindElement(By.ClassName("subtitle-login"));
            entreOuCadastre.Click();

            IWebElement acessarMinhaLista = driver.FindElement(By.XPath("/html/body/div[3]/header/div[2]/div/div/div[6]/div[1]/div/ul/li[3]/a/u"));
            acessarMinhaLista.Click();

            urlPaginaAtual = driver.Url;
            Assert.AreEqual(urlPaginaAtual, urlPaginaLogin, "\nA URL esperada é diferente da URL apresentada pelo navegador.");

            IWebElement inserirEmail = driver.FindElement(By.Id("header-sign-up-link"));
            inserirEmail.Click();

            urlPaginaAtual = driver.Url;
            Assert.AreEqual(urlPaginaAtual, urlPaginaCadastro, "\nA URL esperada é diferente da URL apresentada pelo navegador.");

            IWebElement nome = driver.FindElement(By.Id("pre-sign-up-form-first-name"));
            nome.SendKeys("André");

            IWebElement email = driver.FindElement(By.Id("pre-sign-up-form-email"));
            email.SendKeys("exemplo311@exemplo.com");

            IWebElement cpf = driver.FindElement(By.Id("pre-sign-up-form-cpf"));
            cpf.SendKeys(GerarCpf());

            IWebElement senha = driver.FindElement(By.Id("pre-sign-up-form-password"));
            senha.SendKeys("Teste@Teste");

            IWebElement termos = driver.FindElement(By.Id("pre-sign-up-terms"));
            termos.Click();

            IWebElement cadastrar = driver.FindElement(By.Id("btn-pre-sign-up-new"));
            cadastrar.Click();

            urlPaginaAtual = driver.Url;
            Assert.AreEqual(urlPaginaAtual, urlCadastroCompleto, "\nA URL esperada é diferente da URL apresentada pelo navegador.");

            Thread.Sleep(2000);

            driver.Quit();
        }

        public static String GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

    }
}