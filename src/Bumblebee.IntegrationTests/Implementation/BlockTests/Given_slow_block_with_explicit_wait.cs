using System.Threading;

using Bumblebee.Implementation;
using Bumblebee.IntegrationTests.Shared.Hosting;
using Bumblebee.IntegrationTests.Shared.Pages;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using Bumblebee.Setup.DriverEnvironments;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

namespace Bumblebee.IntegrationTests.Implementation
{
    public class Given_slow_block_with_explicit_wait : HostTestFixture
    {
        [SetUp]
        public void TestSetUp()
        {
            Threaded<Session>
                .With<HeadlessChrome>()
                .NavigateTo<SlowBlockPage>(GetUrl("SlowBlock.html"));
        }

        [TearDown]
        public void TestDispose()
        {
            Threaded<Session>
                .CurrentBlock<SlowBlockPage>()
                .Session.End();
        }

        [Test]
        public void When_getting_text_of_textfield_using_wait_Should_wait()
        {
            Threaded<Session>
                .CurrentBlock<SlowBlockPage>()
                .CustomerInfoWithExplicitWait
                .FirstName
                .Text
                .Should().Be("Todd");
        }

        [Test]
        public void NavigateToGitHub()
        {
	        var gitHub = Threaded<Session>.With<Chrome>().NavigateTo<GitHub>("https://github.com/Bumblebee/Bumblebee");

			Thread.Sleep(4000);

			gitHub.Press<Page>(Key.Slash);
			Thread.Sleep(4000);
        }

        public class GitHub : Page
        {
	        public GitHub(Session session) : base(session)
	        {
	        }
        }
    }
}