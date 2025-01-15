using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200008C RID: 140
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class JsonContract
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001C9EF File Offset: 0x0001ABEF
		public Type UnderlyingType { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001C9F7 File Offset: 0x0001ABF7
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001CA00 File Offset: 0x0001AC00
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
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001CA54 File Offset: 0x0001AC54
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		public bool? IsReference { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001CA65 File Offset: 0x0001AC65
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001CA6D File Offset: 0x0001AC6D
		[Nullable(2)]
		public JsonConverter Converter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001CA76 File Offset: 0x0001AC76
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001CA7E File Offset: 0x0001AC7E
		[Nullable(2)]
		public JsonConverter InternalConverter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			internal set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001CA87 File Offset: 0x0001AC87
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
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001CAA2 File Offset: 0x0001ACA2
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
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001CABD File Offset: 0x0001ACBD
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
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001CAD8 File Offset: 0x0001ACD8
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
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001CAF3 File Offset: 0x0001ACF3
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
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001CB0E File Offset: 0x0001AD0E
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001CB16 File Offset: 0x0001AD16
		[Nullable(new byte[] { 2, 1 })]
		public Func<object> DefaultCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001CB1F File Offset: 0x0001AD1F
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001CB27 File Offset: 0x0001AD27
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x060006CE RID: 1742 RVA: 0x0001CB30 File Offset: 0x0001AD30
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

		// Token: 0x060006CF RID: 1743 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
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

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001CC24 File Offset: 0x0001AE24
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

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001CC80 File Offset: 0x0001AE80
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

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001CCDC File Offset: 0x0001AEDC
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

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001CD38 File Offset: 0x0001AF38
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

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001CD94 File Offset: 0x0001AF94
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[] { context });
			};
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001CDAD File Offset: 0x0001AFAD
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[] { context, econtext });
			};
		}

		// Token: 0x0400026A RID: 618
		internal bool IsNullable;

		// Token: 0x0400026B RID: 619
		internal bool IsConvertable;

		// Token: 0x0400026C RID: 620
		internal bool IsEnum;

		// Token: 0x0400026D RID: 621
		internal Type NonNullableUnderlyingType;

		// Token: 0x0400026E RID: 622
		internal ReadType InternalReadType;

		// Token: 0x0400026F RID: 623
		internal JsonContractType ContractType;

		// Token: 0x04000270 RID: 624
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x04000271 RID: 625
		internal bool IsSealed;

		// Token: 0x04000272 RID: 626
		internal bool IsInstantiable;

		// Token: 0x04000273 RID: 627
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x04000274 RID: 628
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x04000275 RID: 629
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x04000276 RID: 630
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x04000277 RID: 631
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x04000278 RID: 632
		private Type _createdType;
	}
}
