using System;
using System.Reflection;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200091E RID: 2334
	internal class MqColumnInfo
	{
		// Token: 0x0600428E RID: 17038 RVA: 0x000E07BC File Offset: 0x000DE9BC
		public MqColumnInfo(MqColumn column, string name, TypeValue type, MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool> unmarshaller = null, PropertyInfo property = null, int? maxLength = null, bool isFoldable = false, bool isWritable = false)
		{
			this.Column = column;
			this.Name = name;
			this.Type = type;
			this.Unmarshaller = unmarshaller;
			this.IsFoldable = isFoldable;
			this.IsWritable = isWritable;
			this.property = property;
			this.maxLength = maxLength;
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x000E080C File Offset: 0x000DEA0C
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x000E0814 File Offset: 0x000DEA14
		public bool IsFoldable { get; private set; }

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000E081D File Offset: 0x000DEA1D
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x000E0825 File Offset: 0x000DEA25
		public bool IsWritable { get; private set; }

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x000E082E File Offset: 0x000DEA2E
		// (set) Token: 0x06004294 RID: 17044 RVA: 0x000E0836 File Offset: 0x000DEA36
		public TypeValue Type { get; private set; }

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000E083F File Offset: 0x000DEA3F
		// (set) Token: 0x06004296 RID: 17046 RVA: 0x000E0847 File Offset: 0x000DEA47
		public string Name { get; private set; }

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x000E0850 File Offset: 0x000DEA50
		// (set) Token: 0x06004298 RID: 17048 RVA: 0x000E0858 File Offset: 0x000DEA58
		public MqColumn Column { get; private set; }

		// Token: 0x06004299 RID: 17049 RVA: 0x000E0861 File Offset: 0x000DEA61
		public void SetProperty(Message message, object value)
		{
			if (this.property != null)
			{
				this.property.SetValue(message.RealValue, value, null);
			}
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x000E0884 File Offset: 0x000DEA84
		public bool TryConvertIntoObject(Value value, out object objectValue)
		{
			if (this.Unmarshaller != null)
			{
				return this.Unmarshaller(value, this.maxLength, out objectValue);
			}
			objectValue = null;
			return false;
		}

		// Token: 0x040022F7 RID: 8951
		private readonly MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool> Unmarshaller;

		// Token: 0x040022F8 RID: 8952
		private readonly PropertyInfo property;

		// Token: 0x040022F9 RID: 8953
		private readonly int? maxLength;

		// Token: 0x0200091F RID: 2335
		// (Invoke) Token: 0x0600429C RID: 17052
		public delegate TResult TryUnmarshallValue<T1, T2, T3, TResult>(T1 marshalledValue, T2 maxLength, out T3 unmarshalledValue);
	}
}
