﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008B RID: 139
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonContract
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001C9C3 File Offset: 0x0001ABC3
		public Type UnderlyingType { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001C9CB File Offset: 0x0001ABCB
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001C9D4 File Offset: 0x0001ABD4
		public Type CreatedType
		{
			get
			{
				return this._createdType;
			}
			set
			{
				ValidationUtils.ArgumentNotNull(value, "value");
				this._createdType = value;
				this.IsSealed = this._createdType.IsSealed();
				this.IsInstantiable = !this._createdType.IsInterface() && !this._createdType.IsAbstract();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001CA28 File Offset: 0x0001AC28
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0001CA30 File Offset: 0x0001AC30
		public bool? IsReference { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001CA39 File Offset: 0x0001AC39
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001CA41 File Offset: 0x0001AC41
		[Nullable(2)]
		public JsonConverter Converter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001CA4A File Offset: 0x0001AC4A
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001CA52 File Offset: 0x0001AC52
		[Nullable(2)]
		public JsonConverter InternalConverter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			internal set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001CA5B File Offset: 0x0001AC5B
		public IList<SerializationCallback> OnDeserializedCallbacks
		{
			get
			{
				if (this._onDeserializedCallbacks == null)
				{
					this._onDeserializedCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializedCallbacks;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001CA76 File Offset: 0x0001AC76
		public IList<SerializationCallback> OnDeserializingCallbacks
		{
			get
			{
				if (this._onDeserializingCallbacks == null)
				{
					this._onDeserializingCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializingCallbacks;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001CA91 File Offset: 0x0001AC91
		public IList<SerializationCallback> OnSerializedCallbacks
		{
			get
			{
				if (this._onSerializedCallbacks == null)
				{
					this._onSerializedCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializedCallbacks;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		public IList<SerializationCallback> OnSerializingCallbacks
		{
			get
			{
				if (this._onSerializingCallbacks == null)
				{
					this._onSerializingCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializingCallbacks;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001CAC7 File Offset: 0x0001ACC7
		public IList<SerializationErrorCallback> OnErrorCallbacks
		{
			get
			{
				if (this._onErrorCallbacks == null)
				{
					this._onErrorCallbacks = new List<SerializationErrorCallback>();
				}
				return this._onErrorCallbacks;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001CAE2 File Offset: 0x0001ACE2
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0001CAEA File Offset: 0x0001ACEA
		[Nullable(new byte[] { 2, 1 })]
		public Func<object> DefaultCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001CAF3 File Offset: 0x0001ACF3
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001CAFB File Offset: 0x0001ACFB
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x060006CD RID: 1741 RVA: 0x0001CB04 File Offset: 0x0001AD04
		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			underlyingType = ReflectionUtils.EnsureNotByRefType(underlyingType);
			this.IsNullable = ReflectionUtils.IsNullable(underlyingType);
			this.NonNullableUnderlyingType = ((this.IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			this._createdType = (this.CreatedType = this.NonNullableUnderlyingType);
			this.IsConvertable = ConvertUtils.IsConvertible(this.NonNullableUnderlyingType);
			this.IsEnum = this.NonNullableUnderlyingType.IsEnum();
			this.InternalReadType = ReadType.Read;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (this._onSerializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001CBF8 File Offset: 0x0001ADF8
		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (this._onSerializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001CC54 File Offset: 0x0001AE54
		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (this._onDeserializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (this._onDeserializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (this._onErrorCallbacks != null)
			{
				foreach (SerializationErrorCallback serializationErrorCallback in this._onErrorCallbacks)
				{
					serializationErrorCallback(o, context, errorContext);
				}
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001CD68 File Offset: 0x0001AF68
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[] { context });
			};
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001CD81 File Offset: 0x0001AF81
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[] { context, econtext });
			};
		}

		// Token: 0x04000269 RID: 617
		internal bool IsNullable;

		// Token: 0x0400026A RID: 618
		internal bool IsConvertable;

		// Token: 0x0400026B RID: 619
		internal bool IsEnum;

		// Token: 0x0400026C RID: 620
		internal Type NonNullableUnderlyingType;

		// Token: 0x0400026D RID: 621
		internal ReadType InternalReadType;

		// Token: 0x0400026E RID: 622
		internal JsonContractType ContractType;

		// Token: 0x0400026F RID: 623
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x04000270 RID: 624
		internal bool IsSealed;

		// Token: 0x04000271 RID: 625
		internal bool IsInstantiable;

		// Token: 0x04000272 RID: 626
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x04000273 RID: 627
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x04000274 RID: 628
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x04000275 RID: 629
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x04000276 RID: 630
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x04000277 RID: 631
		private Type _createdType;
	}
}
