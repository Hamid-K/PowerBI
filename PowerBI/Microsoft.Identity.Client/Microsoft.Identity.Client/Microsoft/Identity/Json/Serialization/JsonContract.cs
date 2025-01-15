using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200008B RID: 139
	internal abstract class JsonContract
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001C41B File Offset: 0x0001A61B
		public Type UnderlyingType { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001C423 File Offset: 0x0001A623
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001C42C File Offset: 0x0001A62C
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
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001C480 File Offset: 0x0001A680
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001C488 File Offset: 0x0001A688
		public bool? IsReference { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001C491 File Offset: 0x0001A691
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001C499 File Offset: 0x0001A699
		[Nullable(2)]
		public JsonConverter Converter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001C4A2 File Offset: 0x0001A6A2
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001C4AA File Offset: 0x0001A6AA
		[Nullable(2)]
		public JsonConverter InternalConverter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			internal set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001C4B3 File Offset: 0x0001A6B3
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
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001C4CE File Offset: 0x0001A6CE
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
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001C4E9 File Offset: 0x0001A6E9
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
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001C504 File Offset: 0x0001A704
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
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001C51F File Offset: 0x0001A71F
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
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001C53A File Offset: 0x0001A73A
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001C542 File Offset: 0x0001A742
		[Nullable(new byte[] { 2, 0 })]
		public Func<object> DefaultCreator
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001C54B File Offset: 0x0001A74B
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001C553 File Offset: 0x0001A753
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001C55C File Offset: 0x0001A75C
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

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
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

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001C650 File Offset: 0x0001A850
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

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001C6AC File Offset: 0x0001A8AC
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

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001C708 File Offset: 0x0001A908
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

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001C764 File Offset: 0x0001A964
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

		// Token: 0x060006CA RID: 1738 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[] { context });
			};
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001C7D9 File Offset: 0x0001A9D9
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[] { context, econtext });
			};
		}

		// Token: 0x0400024F RID: 591
		internal bool IsNullable;

		// Token: 0x04000250 RID: 592
		internal bool IsConvertable;

		// Token: 0x04000251 RID: 593
		internal bool IsEnum;

		// Token: 0x04000252 RID: 594
		internal Type NonNullableUnderlyingType;

		// Token: 0x04000253 RID: 595
		internal ReadType InternalReadType;

		// Token: 0x04000254 RID: 596
		internal JsonContractType ContractType;

		// Token: 0x04000255 RID: 597
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x04000256 RID: 598
		internal bool IsSealed;

		// Token: 0x04000257 RID: 599
		internal bool IsInstantiable;

		// Token: 0x04000258 RID: 600
		[Nullable(new byte[] { 2, 0 })]
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x04000259 RID: 601
		[Nullable(new byte[] { 2, 0 })]
		private List<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x0400025A RID: 602
		[Nullable(new byte[] { 2, 0 })]
		private List<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x0400025B RID: 603
		[Nullable(new byte[] { 2, 0 })]
		private List<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x0400025C RID: 604
		[Nullable(new byte[] { 2, 0 })]
		private List<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x0400025D RID: 605
		private Type _createdType;
	}
}
