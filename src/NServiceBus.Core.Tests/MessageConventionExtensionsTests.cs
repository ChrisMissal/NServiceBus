﻿namespace NServiceBus.Core.Tests.Encryption
{
    using System;
    using NUnit.Framework;
    using Conventions = NServiceBus.Conventions;

    [TestFixture]
    public class MessageConventionExtensionsTests
    {
        [Test]
        public void Should_use_TimeToBeReceived_from_bottom_of_tree()
        {
            var conventions = new Conventions();
            var timeToBeReceivedAction = conventions.TimeToBeReceivedAction(typeof(InheritedClassWithAttribute));
            Assert.AreEqual(TimeSpan.FromSeconds(2), timeToBeReceivedAction);
        }
        [Test]
        public void Should_use_inherited_TimeToBeReceived()
        {
            var conventions = new Conventions();
            var timeToBeReceivedAction = conventions.TimeToBeReceivedAction(typeof(InheritedClassWithNoAttribute));
            Assert.AreEqual(TimeSpan.FromSeconds(1), timeToBeReceivedAction);
        }

        [TimeToBeReceivedAttribute("00:00:01")]
        class BaseClass
        {
        }
        [TimeToBeReceivedAttribute("00:00:02")]
        class InheritedClassWithAttribute : BaseClass
        {
            
        }
        class InheritedClassWithNoAttribute : BaseClass
        {
            
        }
    }

}