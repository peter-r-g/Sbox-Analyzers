﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Rules
// Assembly: Sandbox.Access, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DFCFBC9-66F4-43AC-BC15-AB403E66F12A
// Assembly location: D:\SteamLibrary\steamapps\common\sbox\bin\managed\Sandbox.Access.dll
// XML documentation location: D:\SteamLibrary\steamapps\common\sbox\bin\managed\Sandbox.Access.xml
// Decompiled 12:09PM EST on 05/05/2023

namespace Sandbox
{
	internal static class Rules
	{
		internal static string[] Async = new string[26]
		{
		  "System.Private.CoreLib/System.IAsyncResult",
		  "System.Private.CoreLib/System.AsyncCallback",
		  "System.Private.CoreLib/System.Threading.Tasks.Task",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.Yield()",
		  "System.Private.CoreLib/System.Threading.Tasks.Task`1",
		  "System.Private.CoreLib/System.Threading.Tasks.Task`1.*",
		  "System.Private.CoreLib/System.Threading.Tasks.ValueTask*",
		  "System.Private.CoreLib/System.Threading.Tasks.TaskCanceledException*",
		  "System.Private.CoreLib/System.Runtime.CompilerServices.TaskAwaiter*",
		  "System.Private.CoreLib/System.Runtime.CompilerServices.AsyncTaskMethodBuilder*",
		  "System.Private.CoreLib/System.Runtime.CompilerServices.YieldAwaitable*",
		  "System.Private.CoreLib/System.Runtime.CompilerServices.IAsyncStateMachine",
		  "System.Private.CoreLib/System.Runtime.CompilerServices.AsyncVoidMethodBuilder*",
		  "System.Private.CoreLib/System.Runtime.CompilerServices.ValueTaskAwaiter*",
		  "System.Private.CoreLib/System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder*",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.Delay*",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.FromCanceled*",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.FromException*",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.FromResult*",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.GetAwaiter*",
		  "System.Private.CoreLib/System.Threading.Tasks.TaskCompletionSource",
		  "System.Private.CoreLib/System.Threading.Tasks.TaskCompletionSource.*",
		  "System.Private.CoreLib/System.Threading.Tasks.TaskCompletionSource`1",
		  "System.Private.CoreLib/System.Threading.Tasks.TaskCompletionSource`1.*",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.get_CompletedTask()",
		  "System.Private.CoreLib/System.Threading.Tasks.Task.get_IsCompleted()"
		};
		internal static string[] BaseAccess = new string[153]
		{
	  "Sandbox.Engine/*",
	  "Sandbox.Game/*",
	  "Sandbox.System/*",
	  "Sandbox.Event/*",
	  "Sandbox.Bind/*",
	  "Sandbox.Reflection/*",
	  "System.Private.CoreLib/System.IDisposable*",
	  "System.Private.CoreLib/System.Collections.*",
	  "System.Collections/System.Collections.*",
	  "System.Collections.Immutable/System.Collections.Immutable.*",
	  "System.ObjectModel/System.Collections.ObjectModel.*",
	  "System.ObjectModel/System.Collections.Specialized.*",
	  "System.Private.CoreLib/System.Math*",
	  "System.Private.CoreLib/System.Numerics*",
	  "System.Private.CoreLib/System.Globalization*",
	  "System.Linq/*",
	  "System.Private.CoreLib/System.IEquatable*",
	  "System.Private.CoreLib/System.IComparable*",
	  "System.Private.CoreLib/System.Comparison*",
	  "System.Private.CoreLib/System.IO.BinaryWriter*",
	  "System.Private.CoreLib/System.IO.BinaryReader*",
	  "System.Private.CoreLib/System.Activator.CreateInstance<T>()",
	  "System.Private.CoreLib/System.Guid*",
	  "System.Private.CoreLib/System.IO.MemoryStream*",
	  "System.Private.CoreLib/System.IO.Stream",
	  "System.Private.CoreLib/System.IO.Stream.*",
	  "System.Private.CoreLib/System.Threading.CancellationToken*",
	  "System.Private.CoreLib/System.Threading.CancellationTokenSource*",
	  "System.Private.CoreLib/System.Enum*",
	  "System.Private.CoreLib/System.Environment.get_CurrentManagedThreadId()",
	  "System.Private.CoreLib/System.Environment.get_StackTrace()",
	  "System.Private.CoreLib/System.DateTime*",
	  "System.Private.CoreLib/System.DayOfWeek*",
	  "System.Private.CoreLib/System.HashCode*",
	  "System.Private.CoreLib/System.StringSplitOptions*",
	  "System.Private.CoreLib/System.ValueTuple*",
	  "System.Private.CoreLib/System.Tuple*",
	  "System.Private.CoreLib/System.Random*",
	  "System.Private.CoreLib/System.MemoryExtensions*",
	  "System.Private.CoreLib/System.IFormatProvider",
	  "System.Private.CoreLib/System.Version*",
	  "System.Private.CoreLib/System.MidpointRounding*",
	  "System.Private.CoreLib/System.Lazy*",
	  "System.Private.CoreLib/System.Threading.Interlocked*",
	  "System.Private.CoreLib/System.Threading.Monitor.Enter(*",
	  "System.Private.CoreLib/System.Threading.Monitor.Exit(*",
	  "System.Private.CoreLib/System.Delegate",
	  "System.Private.CoreLib/System.Delegate.Combine(*",
	  "System.Private.CoreLib/System.Delegate.Remove(*",
	  "System.Private.CoreLib/System.GC.SuppressFinalize(*",
	  "System.Private.CoreLib/System.RuntimeFieldHandle",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray( System.Array, System.RuntimeFieldHandle )",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.RuntimeHelpers.EnsureSufficientExecutionStack()",
	  "System.Private.CoreLib/System.IO.Path.*",
	  "System.Private.CoreLib/System.IO.FileMode",
	  "System.Private.CoreLib/System.IO.SeekOrigin",
	  "System.Private.CoreLib/System.Text.*",
	  "System.Text.RegularExpressions/System.Text.RegularExpressions.*",
	  "System.Private.CoreLib/System.Buffers.ArrayPool*",
	  "System.Private.CoreLib/System.Convert.ToInt32*",
	  "System.Private.CoreLib/System.TimeSpan*",
	  "System.Private.CoreLib/System.RuntimeTypeHandle",
	  "System.Private.CoreLib/System.StringComparison",
	  "System.Private.CoreLib/System.Attribute*",
	  "System.Private.CoreLib/System.AttributeUsageAttribute*",
	  "System.Private.CoreLib/System.FlagsAttribute*",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.TupleElementNamesAttribute*",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.IsReadOnlyAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.IsExternalInit",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.ExtensionAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.IteratorStateMachineAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.AsyncStateMachineAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.CompilerGeneratedAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.IsByRefLikeAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.RequiredMemberAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute",
	  "System.Private.CoreLib/System.Runtime.InteropServices.InAttribute",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.DefaultInterpolatedStringHandler*",
	  "System.Private.CoreLib/System.ComponentModel.EditorBrowsableAttribute*",
	  "System.Text.Json/System.Text.Json.Serialization.JsonPropertyNameAttribute*",
	  "System.Private.CoreLib/System.ObsoleteAttribute*",
	  "System.Private.CoreLib/System.Diagnostics.DebuggerDisplayAttribute*",
	  "System.Private.CoreLib/System.Diagnostics.ConditionalAttribute*",
	  "System.Private.CoreLib/System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute",
	  "System.Private.CoreLib/System.ThreadStaticAttribute*",
	  "System.Private.CoreLib/System.Span*",
	  "System.Private.CoreLib/System.ReadOnlySpan*",
	  "System.Private.CoreLib/System.Reflection.DefaultMemberAttribute*",
	  "System.Private.CoreLib/System.Index*",
	  "System.Private.CoreLib/System.Range*",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.RuntimeHelpers.GetSubArray*",
	  "System.Text.Json/System.Text.Json.*",
	  "System.Private.CoreLib/System.BitConverter*",
	  "System.Private.CoreLib/System.Convert*",
	  "System.IO.Compression/System.IO.Compression.DeflateStream*",
	  "System.IO.Compression/System.IO.Compression.GZipStream*",
	  "System.IO.Compression/System.IO.Compression.CompressionMode",
	  "System.IO.Compression/System.IO.Compression.CompressionLevel",
	  "System.IO.Compression/System.IO.Compression.ZipArchive*",
	  "System.IO.Compression/System.IO.Compression.ZipArchiveEntry*",
	  "System.IO.Compression/System.IO.Compression.ZipArchiveMode",
	  "System.Private.CoreLib/System.Net.WebUtility*",
	  "System.Private.Uri/System.Uri*",
	  "System.Threading.Channels/System.Threading.Channels.*",
	  "System.ComponentModel.Primitives/System.ComponentModel.*",
	  "System.ComponentModel.Annotations/System.ComponentModel.DataAnnotations.*",
	  "System.Private.CoreLib/System.EventArgs*",
	  "System.Private.CoreLib/System.EventHandler*",
	  "System.Web.HttpUtility/System.Web.HttpUtility*",
	  "System.Collections.Specialized/System.Collections.Specialized.*",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.FormattableStringFactory*",
	  "System.Private.CoreLib/System.FormattableString*",
	  "System.Private.CoreLib/System.IO.StreamReader",
	  "System.Private.CoreLib/System.IO.StreamReader..ctor( System.IO.Stream*",
	  "System.Private.CoreLib/System.IO.StreamReader.Close()",
	  "System.Private.CoreLib/System.IO.StreamReader.Peek*",
	  "System.Private.CoreLib/System.IO.StreamReader.Read*",
	  "System.Private.CoreLib/System.IO.StreamReader.get_*",
	  "System.Private.CoreLib/System.IO.TextReader*",
	  "System.Private.CoreLib/System.IO.TextWriter*",
	  "System.Private.CoreLib/System.IO.StringWriter*",
	  "System.Private.CoreLib/System.Buffers.Binary.BinaryPrimitives*",
	  "System.Private.CoreLib/System.Buffer.BlockCopy( System.Array, System.Int32, System.Array, System.Int32, System.Int32 )",
	  "System.Private.CoreLib/System.Buffer.ByteLength( System.Array )",
	  "System.Private.CoreLib/System.Buffer.GetByte( System.Array, System.Int32 )",
	  "System.Private.CoreLib/System.Buffer.SetByte( System.Array, System.Int32, System.Byte )",
	  "System.Private.CoreLib/System.WeakReference",
	  "System.Private.CoreLib/System.WeakReference..ctor( System.Object*",
	  "System.Private.CoreLib/System.WeakReference.*",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.ConditionalWeakTable*",
	  "System.Private.CoreLib/System.ValueType*",
	  "System.Private.CoreLib/System.IConvertible*",
	  "System.Private.CoreLib/System.TimeZoneInfo*",
	  "System.Security.Cryptography/System.Security.Cryptography.HashAlgorithm*",
	  "System.Security.Cryptography/System.Security.Cryptography.MD5*",
	  "System.Security.Cryptography/System.Security.Cryptography.SHA1*",
	  "System.Security.Cryptography/System.Security.Cryptography.SHA256*",
	  "System.Security.Cryptography/System.Security.Cryptography.SHA512*",
	  "System.Net.Http/System.Net.Http.HttpResponseMessage*",
	  "System.Net.Http/System.Net.Http.HttpContent*",
	  "System.Net.Http/System.Net.Http.ByteArrayContent*",
	  "System.Net.Http/System.Net.Http.StringContent*",
	  "System.Net.Http/System.Net.Http.FormUrlEncodedContent*",
	  "System.Net.Http/System.Net.Http.StreamContent*",
	  "System.Net.Http/System.Net.Http.MultipartContent*",
	  "System.Net.Http/System.Net.Http.Headers.HttpHeaders*",
	  "System.Net.Http/System.Net.Http.Headers.HttpContentHeaders*",
	  "System.Net.Http/System.Net.Http.Headers.HttpResponseHeaders*",
	  "System.Net.Http.Json/System.Net.Http.Json.JsonContent*",
	  "System.Net.Primitives/System.Net.HttpStatusCode",
	  "System.Collections.Concurrent/System.Collections.Concurrent.ConcurrentDictionary*",
	  "System.Collections.Concurrent/System.Collections.Concurrent.BlockingCollection*"
		};
		internal static string[] Diagnostics = new string[6]
		{
	  "System.Private.CoreLib/System.Diagnostics.Stopwatch*",
	  "System.Private.CoreLib/System.Diagnostics.DebuggerBrowsableAttribute*",
	  "System.Private.CoreLib/System.Diagnostics.DebuggerHiddenAttribute*",
	  "System.Private.CoreLib/System.Diagnostics.DebuggerStepThroughAttribute*",
	  "System.Private.CoreLib/System.Diagnostics.StackTraceHiddenAttribute",
	  "System.Private.CoreLib/System.Diagnostics.UnreachableException*"
		};
		internal static string[] Exceptions = new string[20]
		{
	  "System.Private.CoreLib/System.Exception*",
	  "System.Private.CoreLib/System.NotImplementedException*",
	  "System.Private.CoreLib/System.NotSupportedException*",
	  "System.Private.CoreLib/System.IndexOutOfRangeException*",
	  "System.Private.CoreLib/System.FormatException*",
	  "System.Private.CoreLib/System.Runtime.CompilerServices.SwitchExpressionException*",
	  "System.Private.CoreLib/System.ArgumentException*",
	  "System.Private.CoreLib/System.ArgumentNullException*",
	  "System.Private.CoreLib/System.ArgumentOutOfRangeException*",
	  "System.Private.CoreLib/System.InvalidCastException*",
	  "System.Private.CoreLib/System.InvalidOperationException*",
	  "System.Private.CoreLib/System.PlatformNotSupportedException*",
	  "System.Private.CoreLib/System.UnauthorizedAccessException*",
	  "System.Private.CoreLib/System.IO.IOException*",
	  "System.Private.CoreLib/System.IO.FileNotFoundException*",
	  "System.Private.CoreLib/System.IO.DirectoryNotFoundException*",
	  "System.Private.CoreLib/System.IO.FileLoadException*",
	  "System.Private.CoreLib/System.IO.PathTooLongException*",
	  "System.Private.CoreLib/System.IO.EndOfStreamException*",
	  "System.Private.CoreLib/System.DivideByZeroException*"
		};
		internal static string[] Reflection = new string[32]
		{
	  "System.Private.CoreLib/System.Reflection.CustomAttributeExtensions*",
	  "System.Private.CoreLib/System.Reflection.BindingFlags*",
	  "System.Private.CoreLib/System.Reflection.MemberInfo",
	  "System.Private.CoreLib/System.Reflection.MemberInfo.get_Name()",
	  "System.Private.CoreLib/System.Reflection.MemberInfo.get_Name()",
	  "System.Private.CoreLib/System.Reflection.MemberInfo.IsDefined( System.Type, System.Boolean )",
	  "System.Private.CoreLib/System.Reflection.MemberInfo.get_DeclaringType()",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo.GetSetMethod()",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo.GetGetMethod()",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo.get_CanWrite()",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo.get_CanRead()",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo.get_PropertyType()",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo.op_Inequality( System.Reflection.PropertyInfo, System.Reflection.PropertyInfo )",
	  "System.Private.CoreLib/System.Reflection.PropertyInfo.op_Inequality( System.Reflection.PropertyInfo, System.Reflection.PropertyInfo )",
	  "System.Private.CoreLib/System.Reflection.MethodInfo",
	  "System.Private.CoreLib/System.Reflection.MethodInfo.op_Equality*",
	  "System.Private.CoreLib/System.Reflection.MethodInfo.get_Name()",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo.get_Name*",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo.get_DefaultValue()",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo.get_IsOptional()",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo.get_ParameterType()",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo.get_IsIn()",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo.get_IsOut()",
	  "System.Private.CoreLib/System.Reflection.ParameterInfo.GetCustomAttributesData()",
	  "System.Private.CoreLib/System.Reflection.CustomAttributeData",
	  "System.Private.CoreLib/System.Reflection.CustomAttributeData.get_AttributeType()",
	  "System.Private.CoreLib/System.Reflection.CustomAttributeData.get_NamedArguments()",
	  "System.Private.CoreLib/System.Reflection.CustomAttributeData.get_ConstructorArguments()",
	  "System.Private.CoreLib/System.Reflection.CustomAttributeTypedArgument*",
	  "System.Private.CoreLib/System.Reflection.CustomAttributeNamedArgument*"
		};
		internal static string[] Types = new string[40]
		{
	  "System.Private.CoreLib/System.Object*",
	  "System.Private.CoreLib/System.Void*",
	  "System.Private.CoreLib/System.Boolean*",
	  "System.Private.CoreLib/System.Double*",
	  "System.Private.CoreLib/System.Decimal*",
	  "System.Private.CoreLib/System.Int16*",
	  "System.Private.CoreLib/System.UInt16*",
	  "System.Private.CoreLib/System.Int32*",
	  "System.Private.CoreLib/System.UInt32*",
	  "System.Private.CoreLib/System.UInt64*",
	  "System.Private.CoreLib/System.Int64*",
	  "System.Private.CoreLib/System.IntPtr*",
	  "System.Private.CoreLib/System.Single*",
	  "System.Private.CoreLib/System.Char*",
	  "System.Private.CoreLib/System.Byte*",
	  "System.Private.CoreLib/System.SByte*",
	  "System.Private.CoreLib/System.String*",
	  "System.Private.CoreLib/System.Array*",
	  "System.Private.CoreLib/System.Half*",
	  "System.Private.CoreLib/System.TypeCode*",
	  "System.Private.CoreLib/System.Action*",
	  "System.Private.CoreLib/System.Func*",
	  "System.Private.CoreLib/System.Nullable*",
	  "System.Private.CoreLib/System.Predicate*",
	  "System.Private.CoreLib/System.Type",
	  "System.Private.CoreLib/System.Type.get_IsEnum()",
	  "System.Private.CoreLib/System.Type.Equals( System.Type )",
	  "System.Private.CoreLib/System.Type.op_Equality( System.Type, System.Type )",
	  "System.Private.CoreLib/System.Type.op_Inequality( System.Type, System.Type )",
	  "System.Private.CoreLib/System.Type.GetTypeFromHandle( System.RuntimeTypeHandle )",
	  "System.Private.CoreLib/System.Type.GetEnumValues()",
	  "System.Private.CoreLib/System.Type.GetEnumNames()",
	  "System.Private.CoreLib/System.Type.IsSubclassOf( System.Type )",
	  "System.Private.CoreLib/System.Type.get_FullName()",
	  "System.Private.CoreLib/System.Type.get_IsAbstract()",
	  "System.Private.CoreLib/System.Type.get_ContainsGenericParameters()",
	  "System.Private.CoreLib/System.Type.get_IsGenericType()",
	  "System.Private.CoreLib/System.Type.IsAssignableTo( System.Type )",
	  "System.Private.CoreLib/System.Type.GetInterfaces()",
	  "System.Private.CoreLib/System.Type.IsAssignableFrom( System.Type )"
		};
	}
}