﻿using System;
using System.Runtime.InteropServices;

using ROS2.Utils;
using ROS2.Interfaces;

namespace rclcs
{
    internal static class NativeMethods
    {
        private static readonly DllLoadUtils dllLoadUtils = DllLoadUtilsFactory.GetDllLoadUtils();
        // --- RCL ---
        private static readonly IntPtr nativeRCL = dllLoadUtils.LoadLibraryNoSuffix("rcl");

        // rcl_get_zero_initialized_context
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_context_t GetZeroInitializedContextType();
        internal static GetZeroInitializedContextType 
            rcl_get_zero_initialized_context = 
            (GetZeroInitializedContextType) Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL, 
            "rcl_get_zero_initialized_context"), 
            typeof(GetZeroInitializedContextType));

        // rcl_get_zero_initialized_init_options
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_init_options_t GetZeroInitializedInitOptionsType();
        internal static GetZeroInitializedInitOptionsType
            rcl_get_zero_initialized_init_options = 
            (GetZeroInitializedInitOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_get_zero_initialized_init_options"),
            typeof(GetZeroInitializedInitOptionsType));

        // rcl_init_options_init
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int InitOptionsInitType(ref rcl_init_options_t init_options, rcl_allocator_t allocator);
        internal static InitOptionsInitType
        rcl_init_options_init =
        (InitOptionsInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_init_options_init"),
        typeof(InitOptionsInitType));

        // rcl_shutdown
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int ShutdownType(ref rcl_context_t context);
        internal static ShutdownType
            rcl_shutdown =
            (ShutdownType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_shutdown"),
            typeof(ShutdownType));

        // rcl_context_is_valid
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool ContextIsValidType(ref rcl_context_t context);
        internal static ContextIsValidType
            rcl_context_is_valid =
            (ContextIsValidType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_context_is_valid"),
            typeof(ContextIsValidType));

        // rcl_init
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int InitType(int argc, [In, Out] string[] argv, ref rcl_init_options_t option, ref rcl_context_t context);
        internal static InitType
            rcl_init =
            (InitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_init"),
            typeof(InitType));

        // rcl_context_fini
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int ContextFiniType(ref rcl_context_t context);
        internal static ContextFiniType
            rcl_context_fini =
            (ContextFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_context_fini"),
            typeof(ContextFiniType));

        // rcl_get_zero_initialized_node
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_node_t GetZeroInitializedNodeType();
        internal static GetZeroInitializedNodeType
            rcl_get_zero_initialized_node =
            (GetZeroInitializedNodeType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_get_zero_initialized_node"),
            typeof(GetZeroInitializedNodeType));

        // rcl_node_get_default_options
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_node_options_t NodeGetDefaultOptionsType();
        internal static NodeGetDefaultOptionsType
            rcl_node_get_default_options =
            (NodeGetDefaultOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_node_get_default_options"),
            typeof(NodeGetDefaultOptionsType));

        // rcl_node_init
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int NodeInitType(ref rcl_node_t node, string name, string node_namespace, ref rcl_context_t context, ref rcl_node_options_t default_options);
        internal static NodeInitType
            rcl_node_init =
            (NodeInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_node_init"),
            typeof(NodeInitType));

        // rcl_node_fini
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int NodeFiniType(ref rcl_node_t node);
        internal static NodeFiniType
            rcl_node_fini =
            (NodeFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_node_fini"),
            typeof(NodeFiniType));

        // rcl_node_get_name
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr NodeGetNameType(ref rcl_node_t node);
        internal static NodeGetNameType
            rcl_node_get_name =
            (NodeGetNameType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_node_get_name"),
            typeof(NodeGetNameType));

        // rcl_node_get_namespace
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr NodeGetNamespaceType(ref rcl_node_t node);
        internal static NodeGetNamespaceType
            rcl_node_get_namespace =
            (NodeGetNamespaceType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_node_get_namespace"),
            typeof(NodeGetNamespaceType));

        // rcl_publisher_get_default_options
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_publisher_options_t PublisherGetDefaultOptionsType();
        internal static PublisherGetDefaultOptionsType
            rcl_publisher_get_default_options =
            (PublisherGetDefaultOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_publisher_get_default_options"),
            typeof(PublisherGetDefaultOptionsType));

        // rcl_get_zero_initialized_publisher
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_publisher_t GetZeroInitiazizedPublisherType();
        internal static GetZeroInitiazizedPublisherType
            rcl_get_zero_initialized_publisher =
            (GetZeroInitiazizedPublisherType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_get_zero_initialized_publisher"),
            typeof(GetZeroInitiazizedPublisherType));

        // rcl_publisher_init
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int PublisherInitType(ref rcl_publisher_t publisher, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, ref rcl_publisher_options_t publisher_options);
        internal static PublisherInitType
            rcl_publisher_init =
            (PublisherInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_publisher_init"),
            typeof(PublisherInitType));

        // rcl_publisher_fini
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int PublisherFiniType(ref rcl_publisher_t publisher, ref rcl_node_t node);
        internal static PublisherFiniType
            rcl_publisher_fini =
            (PublisherFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_publisher_fini"),
            typeof(PublisherFiniType));

        // rcl_publish
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int PublishType(ref rcl_publisher_t publisher, IntPtr message);
        internal static PublishType
            rcl_publish =
            (PublishType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_publish"),
            typeof(PublishType));

        // rcl_get_zero_initialized_subscription
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_subscription_t GetZeroInitializedSubcriptionType();
        internal static GetZeroInitializedSubcriptionType
            rcl_get_zero_initialized_subscription =
            (GetZeroInitializedSubcriptionType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_get_zero_initialized_subscription"),
            typeof(GetZeroInitializedSubcriptionType));

        // rcl_subscription_get_default_options
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_subscription_options_t SubscriptionGetDefaultOptionsType();
        internal static SubscriptionGetDefaultOptionsType
            rcl_subscription_get_default_options =
            (SubscriptionGetDefaultOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_subscription_get_default_options"),
            typeof(SubscriptionGetDefaultOptionsType));

        // rcl_subscription_init
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int SubscriptionInitType(ref rcl_subscription_t subscription, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, ref rcl_subscription_options_t subscription_options);
        internal static SubscriptionInitType
            rcl_subscription_init =
            (SubscriptionInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_subscription_init"),
            typeof(SubscriptionInitType));

        // rcl_subscription_fini
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int SubscriptionFiniType(ref rcl_subscription_t subscription, ref rcl_node_t node);
        internal static SubscriptionFiniType
            rcl_subscription_fini =
            (SubscriptionFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_subscription_fini"),
            typeof(SubscriptionFiniType));

        // rcl_subscription_is_valid
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool SubscriptionIsValidType(ref rcl_subscription_t subscription);
        internal static SubscriptionIsValidType
            rcl_subscription_is_valid =
            (SubscriptionIsValidType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_subscription_is_valid"),
            typeof(SubscriptionIsValidType));

        // rcl_take
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int TakeType(ref rcl_subscription_t subscription, IntPtr message_handle, IntPtr message_info);
        internal static TakeType
            rcl_take =
            (TakeType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_take"),
            typeof(TakeType));

        // rcl_get_zero_initialized_wait_set
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_wait_set_t GetZeroInitializedWaitSetType();
        internal static GetZeroInitializedWaitSetType
            rcl_get_zero_initialized_wait_set =
            (GetZeroInitializedWaitSetType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_get_zero_initialized_wait_set"),
            typeof(GetZeroInitializedWaitSetType));

        // rcl_wait_set_init
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WaitSetInitType(ref rcl_wait_set_t wait_set,
                                              ulong number_of_subscriptions,
                                              ulong number_of_guard_conditions,
                                              ulong number_of_timers,
                                              ulong number_of_clients,
                                              ulong number_of_services,
                                              rcl_allocator_t allocator);
        internal static WaitSetInitType
            rcl_wait_set_init =
            (WaitSetInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_wait_set_init"),
            typeof(WaitSetInitType));

        // rcl_wait_set_fini
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WatiSetFiniType(ref rcl_wait_set_t wait_set);
        internal static WatiSetFiniType
            rcl_wait_set_fini =
            (WatiSetFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_wait_set_fini"),
            typeof(WatiSetFiniType));

        // rcl_wait_set_clear
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WaitSetClearType(ref rcl_wait_set_t wait_set);
        internal static WaitSetClearType
            rcl_wait_set_clear =
            (WaitSetClearType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_wait_set_clear"),
            typeof(WaitSetClearType));

        // rcl_wait_set_add_subscription
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WaitSetAddSubscriptionType(ref rcl_wait_set_t wait_set, ref rcl_subscription_t subscription, UIntPtr index);
        internal static WaitSetAddSubscriptionType
            rcl_wait_set_add_subscription =
            (WaitSetAddSubscriptionType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_wait_set_add_subscription"),
            typeof(WaitSetAddSubscriptionType));

        // rcl_wait
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WaitType(ref rcl_wait_set_t wait_set, ulong timeout);
        internal static WaitType
            rcl_wait =
            (WaitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCL,
            "rcl_wait"),
            typeof(WaitType));

        // --- RCUtils
        private static readonly IntPtr nativeRCUtils = dllLoadUtils.LoadLibraryNoSuffix("rcutils");

        // rcl_get_default_allocator
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_allocator_t RclGetDefaultAllocatorType();
        internal static RclGetDefaultAllocatorType 
            rcl_get_default_allocator = 
            (RclGetDefaultAllocatorType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCUtils, 
            "rcutils_get_default_allocator"), 
            typeof(RclGetDefaultAllocatorType));

        // rcl_reset_error
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ResetErrorType();
        internal static ResetErrorType
            rcl_reset_error =
            (ResetErrorType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCUtils,
            "rcutils_reset_error"),
            typeof(ResetErrorType));

        // rcl_get_error_string
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate rcl_error_string_t GetErrorStringType();
        internal static GetErrorStringType
            rcl_get_error_string =
            (GetErrorStringType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
            nativeRCUtils,
            "rcutils_get_error_string"),
            typeof(GetErrorStringType));

    }
}
