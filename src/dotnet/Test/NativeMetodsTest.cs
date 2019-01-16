﻿using NUnit.Framework;
using System;
using System.Reflection;
using System.Text;
using rclcs;
using ROS2.Interfaces;
using rclcs.Test;

namespace rclcs.TestNativeMethods
{
    [TestFixture]
    public class RCL
    {
        [Test]
        public void GetZeroInitializedContext()
        {
            rcl_context_t context = NativeMethods.rcl_get_zero_initialized_context();
        }

        [Test]
        public void GetDefaultAllocator()
        {
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
        }

        [Test]
        public void GetZeroInitializedInitOptions()
        {
            rcl_init_options_t init_options = NativeMethods.rcl_get_zero_initialized_init_options();
        }

        [Test]
        public void InitOptionsInit()
        {
            rcl_init_options_t init_options = NativeMethods.rcl_get_zero_initialized_init_options();
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
            int ret = NativeMethods.rcl_init_options_init(ref init_options, allocator);
            Assert.That((RCLReturnEnum)ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));
        }

        [Test]
        public void GetErrorString()
        {
            NativeMethods.rcl_reset_error();
            string messageString = Utils.GetRclErrorString();
            Assert.That(messageString, Is.EqualTo("error not set"));
        }


        [Test]
        public void ResetError()
        {
            NativeMethods.rcl_reset_error();
        }


        [Test]
        public void InitShutdownFinalize()
        {
            RCLReturnEnum ret;
            NativeMethods.rcl_reset_error();
            rcl_init_options_t init_options = NativeMethods.rcl_get_zero_initialized_init_options();
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
            ret = (RCLReturnEnum)NativeMethods.rcl_init_options_init(ref init_options, allocator);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));

            rcl_context_t context = NativeMethods.rcl_get_zero_initialized_context();

            ret = (RCLReturnEnum)NativeMethods.rcl_init(0, null, ref init_options, ref context);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());

            Assert.That(NativeMethods.rcl_context_is_valid(ref context), Is.True);
            ret = (RCLReturnEnum)NativeMethods.rcl_shutdown(ref context);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));

            ret = (RCLReturnEnum)NativeMethods.rcl_context_fini(ref context);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));
        }

        [Test]
        public void InitValidArgs()
        {
            rcl_init_options_t init_options = NativeMethods.rcl_get_zero_initialized_init_options();
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
            NativeMethods.rcl_init_options_init(ref init_options, allocator);
            rcl_context_t context = NativeMethods.rcl_get_zero_initialized_context();

            RCLReturnEnum ret = (RCLReturnEnum)NativeMethods.rcl_init(2, new string[] { "foo", "bar" }, ref init_options, ref context);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));

            Assert.That(NativeMethods.rcl_context_is_valid(ref context), Is.True);
            ret = (RCLReturnEnum)NativeMethods.rcl_shutdown(ref context);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));

            ret = (RCLReturnEnum)NativeMethods.rcl_context_fini(ref context);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));
        }

    }

    [TestFixture]
    public class Node
    {
        rcl_context_t context;

        [SetUp]
        public void SetUp()
        {
            rcl_init_options_t init_options = NativeMethods.rcl_get_zero_initialized_init_options();
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
            NativeMethods.rcl_init_options_init(ref init_options, allocator);
            context = NativeMethods.rcl_get_zero_initialized_context();

            NativeMethods.rcl_init(0, null, ref init_options, ref context);
        }

        [TearDown]
        public void TearDown()
        {
            NativeMethods.rcl_shutdown(ref context);
            NativeMethods.rcl_context_fini(ref context);
        }

        [Test]
        public void GetZeroInitializedNode()
        {
            rcl_node_t node = NativeMethods.rcl_get_zero_initialized_node();
        }

        [Test]
        public void NodeGetDefaultOptions()
        {
            rcl_node_options_t defaultNodeOptions = NativeMethods.rcl_node_get_default_options();
        }

        [Test]
        public void NodeInit()
        {
            rcl_node_t node = NativeMethods.rcl_get_zero_initialized_node();
            rcl_node_options_t defaultNodeOptions = NativeMethods.rcl_node_get_default_options();

            string name = "node_test";
            string nodeNamespace = "/ns";

            RCLReturnEnum ret = (RCLReturnEnum)NativeMethods.rcl_node_init(ref node, name, nodeNamespace, ref context, ref defaultNodeOptions);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));

            NativeMethods.rcl_node_fini(ref node);
        }


        [Test]
        public void NodeGetNamespace()
        {
            rcl_node_t node = NativeMethods.rcl_get_zero_initialized_node();
            rcl_node_options_t defaultNodeOptions = NativeMethods.rcl_node_get_default_options();

            string nodeName = "node_test";
            string nodeNamespace = "/ns";
            RCLReturnEnum ret = (RCLReturnEnum)NativeMethods.rcl_node_init(ref node, nodeName, nodeNamespace, ref context, ref defaultNodeOptions);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK));

            string nodeNameFromRcl = MarshallingHelpers.PtrToString(NativeMethods.rcl_node_get_name(ref node));
            Assert.That("node_test", Is.EqualTo(nodeNameFromRcl));

            NativeMethods.rcl_node_fini(ref node);
        }

        [Test]
        public void NodeGetName()
        {
            rcl_node_t node = NativeMethods.rcl_get_zero_initialized_node();
            rcl_node_options_t defaultNodeOptions = NativeMethods.rcl_node_get_default_options();

            string name = "node_test";
            string nodeNamespace = "/ns";
            NativeMethods.rcl_node_init(ref node, name, nodeNamespace, ref context, ref defaultNodeOptions);

            string nodeNamespaceFromRcl = MarshallingHelpers.PtrToString(NativeMethods.rcl_node_get_namespace(ref node));
            Assert.That("/ns", Is.EqualTo(nodeNamespaceFromRcl));

            NativeMethods.rcl_node_fini(ref node);
        }

        [Test]
        public void NodeIsValid()
        {
            rcl_node_t node = NativeMethods.rcl_get_zero_initialized_node();
            rcl_node_options_t defaultNodeOptions = NativeMethods.rcl_node_get_default_options();

            string name = "node_test";
            string nodeNamespace = "/ns";
            NativeMethods.rcl_node_init(ref node, name, nodeNamespace, ref context, ref defaultNodeOptions);

            NativeMethods.rcl_node_fini(ref node);
        }

    }

    [TestFixture]
    public class Publisher
    {
        rcl_context_t context;
        rcl_node_t node;

        [SetUp]
        public void SetUp()
        {
            rcl_init_options_t init_options = NativeMethods.rcl_get_zero_initialized_init_options();
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
            NativeMethods.rcl_init_options_init(ref init_options, allocator);
            context = NativeMethods.rcl_get_zero_initialized_context();

            NativeMethods.rcl_init(0, null, ref init_options, ref context);

            node = NativeMethods.rcl_get_zero_initialized_node();
            rcl_node_options_t defaultNodeOptions = NativeMethods.rcl_node_get_default_options();

            string name = "publisher_test";
            string nodeNamespace = "/ns";
            NativeMethods.rcl_node_init(ref node, name, nodeNamespace, ref context, ref defaultNodeOptions);
        }

        [TearDown]
        public void TearDown()
        {
            NativeMethods.rcl_node_fini(ref node);
            NativeMethods.rcl_shutdown(ref context);
            NativeMethods.rcl_context_fini(ref context);
        }

        [Test]
        public void PublisherGetDefaultOptions()
        {
            rcl_publisher_options_t publisherOptions = NativeMethods.rcl_publisher_get_default_options();
        }

        [Test]
        public void GetZeroInitializedPublisher()
        {
            rcl_publisher_t publisher = NativeMethods.rcl_get_zero_initialized_publisher();
        }

        [Test]
        public void PublisherInit()
        {
            RCLReturnEnum ret;
            rcl_publisher_t publisher = NativeMethods.rcl_get_zero_initialized_publisher();
            rcl_publisher_options_t publisherOptions = NativeMethods.rcl_publisher_get_default_options();
            MethodInfo m = typeof(std_msgs.msg.Bool).GetTypeInfo().GetDeclaredMethod("_GET_TYPE_SUPPORT");
            IntPtr typeSupportHandle = (IntPtr)m.Invoke(null, new object[] { });
            ret = (RCLReturnEnum)NativeMethods.rcl_publisher_init(ref publisher, ref node, typeSupportHandle, "publisher_test_topic", ref publisherOptions);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());

            ret = (RCLReturnEnum)NativeMethods.rcl_publisher_fini(ref publisher, ref node);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());
        }

        [Test]
        public void PublisherPublish()
        {
            RCLReturnEnum ret;
            rcl_publisher_t publisher = NativeMethods.rcl_get_zero_initialized_publisher();
            rcl_publisher_options_t publisherOptions = NativeMethods.rcl_publisher_get_default_options();
            std_msgs.msg.Bool msg = new std_msgs.msg.Bool();
            IntPtr typeSupportHandle = msg.TypeSupportHandle;
            ret = (RCLReturnEnum)NativeMethods.rcl_publisher_init(ref publisher, ref node, typeSupportHandle, "/publisher_test_topic", ref publisherOptions);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());
            ret = (RCLReturnEnum)NativeMethods.rcl_publish(ref publisher, msg.Handle);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());
            ret = (RCLReturnEnum)NativeMethods.rcl_publisher_fini(ref publisher, ref node);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());
        }

    }

    [TestFixture]
    public class Subscription
    {
        rcl_context_t context;
        rcl_allocator_t allocator;
        rcl_node_t node;

        [SetUp]
        public void SetUp()
        {
            rcl_init_options_t init_options = NativeMethods.rcl_get_zero_initialized_init_options();
            allocator = NativeMethods.rcl_get_default_allocator();
            NativeMethods.rcl_init_options_init(ref init_options, allocator);
            context = NativeMethods.rcl_get_zero_initialized_context();

            NativeMethods.rcl_init(0, null, ref init_options, ref context);

            node = NativeMethods.rcl_get_zero_initialized_node();
            rcl_node_options_t defaultNodeOptions = NativeMethods.rcl_node_get_default_options();

            string name = "subscription_test";
            string nodeNamespace = "/ns";
            NativeMethods.rcl_node_init(ref node, name, nodeNamespace, ref context, ref defaultNodeOptions);
        }

        [TearDown]
        public void TearDown()
        {
            NativeMethods.rcl_node_fini(ref node);
            NativeMethods.rcl_shutdown(ref context);
            NativeMethods.rcl_context_fini(ref context);
        }

        [Test]
        public void GetZeroInitializedSubscription()
        {
            rcl_subscription_t subscription = NativeMethods.rcl_get_zero_initialized_subscription();
        }

        [Test]
        public void SubscriptionGetDefaultOptions()
        {
            rcl_subscription_options_t subscriptionOptions = NativeMethods.rcl_subscription_get_default_options();
        }

        [Test]
        public void SubscriptionInit()
        {
            RCLReturnEnum ret;
            rcl_subscription_t subscription = NativeMethods.rcl_get_zero_initialized_subscription();
            rcl_subscription_options_t subscriptionOptions = NativeMethods.rcl_subscription_get_default_options();
            MethodInfo m = typeof(std_msgs.msg.Bool).GetTypeInfo().GetDeclaredMethod("_GET_TYPE_SUPPORT");
            IntPtr typeSupportHandle = (IntPtr)m.Invoke(null, new object[] { });
            ret = (RCLReturnEnum)NativeMethods.rcl_subscription_init(ref subscription, ref node, typeSupportHandle, "/subscriber_test_topic", ref subscriptionOptions);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());
            ret = (RCLReturnEnum)NativeMethods.rcl_subscription_fini(ref subscription, ref node);
            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());
        }

        [Test]
        public void SubscriptionIsValid()
        {
            RCLReturnEnum ret;
            rcl_subscription_t subscription = NativeMethods.rcl_get_zero_initialized_subscription();
            Assert.That(NativeMethods.rcl_subscription_is_valid(ref subscription), Is.False);
            NativeMethods.rcl_reset_error();

            rcl_subscription_options_t subscriptionOptions = NativeMethods.rcl_subscription_get_default_options();
            MethodInfo m = typeof(std_msgs.msg.Bool).GetTypeInfo().GetDeclaredMethod("_GET_TYPE_SUPPORT");
            IntPtr typeSupportHandle = (IntPtr)m.Invoke(null, new object[] { });
            ret = (RCLReturnEnum)NativeMethods.rcl_subscription_init(ref subscription, ref node, typeSupportHandle, "/subscriber_test_topic", ref subscriptionOptions);
            Assert.That(NativeMethods.rcl_subscription_is_valid(ref subscription), Is.True);

            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_OK), Utils.PopRclErrorString());
            ret = (RCLReturnEnum)NativeMethods.rcl_subscription_fini(ref subscription, ref node);
        }

        [Test]
        public void WaitSetAddSubscription()
        {
            NativeMethods.rcl_reset_error();
            rcl_subscription_t subscription = NativeMethods.rcl_get_zero_initialized_subscription();
            rcl_subscription_options_t subscriptionOptions = NativeMethods.rcl_subscription_get_default_options();
            MethodInfo m = typeof(std_msgs.msg.Bool).GetTypeInfo().GetDeclaredMethod("_GET_TYPE_SUPPORT");
            IntPtr typeSupportHandle = (IntPtr)m.Invoke(null, new object[] { });
            TestUtils.AssertRetOk(NativeMethods.rcl_subscription_init(ref subscription, ref node, typeSupportHandle, "/subscriber_test_topic", ref subscriptionOptions));
            rcl_wait_set_t waitSet = NativeMethods.rcl_get_zero_initialized_wait_set();
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_init(ref waitSet, 1, 0, 0, 0, 0, allocator));
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_clear(ref waitSet));

            Assert.That(NativeMethods.rcl_subscription_is_valid(ref subscription), Is.True);
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_add_subscription(ref waitSet, ref subscription, UIntPtr.Zero));

            RCLReturnEnum ret = (RCLReturnEnum)NativeMethods.rcl_wait(ref waitSet, Utils.TimeoutSecToNsec(0.01));

            Assert.That(ret, Is.EqualTo(RCLReturnEnum.RCL_RET_TIMEOUT));

            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_fini(ref waitSet));
            TestUtils.AssertRetOk(NativeMethods.rcl_subscription_fini(ref subscription, ref node));
        }

    }


    [TestFixture]
    public class WaitSet
    {


        [Test]
        public void GetZeroInitializedWaitSet()
        {
            // NOTE: The struct rcl_wait_set_t contains size_t 
            // fields that are set to UIntPtr in C# declaration,
            // not guaranteed to work for all C implemenations/platforms.
            rcl_wait_set_t waitSet = NativeMethods.rcl_get_zero_initialized_wait_set();
        }

        [Test]
        public void WaitSetInit()
        {
            rcl_wait_set_t waitSet = NativeMethods.rcl_get_zero_initialized_wait_set();
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_init(ref waitSet, 1, 0, 0, 0, 0, allocator));
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_fini(ref waitSet));
        }

        [Test]
        public void WaitSetClear()
        {
            rcl_wait_set_t waitSet = NativeMethods.rcl_get_zero_initialized_wait_set();
            rcl_allocator_t allocator = NativeMethods.rcl_get_default_allocator();
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_init(ref waitSet, 1, 0, 0, 0, 0, allocator));
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_clear(ref waitSet));
            TestUtils.AssertRetOk(NativeMethods.rcl_wait_set_fini(ref waitSet));
        }

    }
}