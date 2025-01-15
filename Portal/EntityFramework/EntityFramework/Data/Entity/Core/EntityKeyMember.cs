using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D1 RID: 721
	[DataContract]
	[Serializable]
	public class EntityKeyMember
	{
		// Token: 0x060022ED RID: 8941 RVA: 0x00063100 File Offset: 0x00061300
		public EntityKeyMember()
		{
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x00063108 File Offset: 0x00061308
		public EntityKeyMember(string keyName, object keyValue)
		{
			Check.NotNull<string>(keyName, "keyName");
			Check.NotNull<object>(keyValue, "keyValue");
			this._keyName = keyName;
			this._keyValue = keyValue;
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x00063136 File Offset: 0x00061336
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0006313E File Offset: 0x0006133E
		[DataMember]
		public string Key
		{
			get
			{
				return this._keyName;
			}
			set
			{
				Check.NotNull<string>(value, "value");
				EntityKeyMember.ValidateWritable(this._keyName);
				this._keyName = value;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x0006315E File Offset: 0x0006135E
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x00063166 File Offset: 0x00061366
		[DataMember]
		public object Value
		{
			get
			{
				return this._keyValue;
			}
			set
			{
				Check.NotNull<object>(value, "value");
				EntityKeyMember.ValidateWritable(this._keyValue);
				this._keyValue = value;
			}
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00063186 File Offset: 0x00061386
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}, {1}]", new object[] { this._keyName, this._keyValue });
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000631AF File Offset: 0x000613AF
		private static void ValidateWritable(object instance)
		{
			if (instance != null)
			{
				throw new InvalidOperationException(Strings.EntityKey_CannotChangeKey);
			}
		}

		// Token: 0x04000C03 RID: 3075
		private string _keyName;

		// Token: 0x04000C04 RID: 3076
		private object _keyValue;
	}
}
