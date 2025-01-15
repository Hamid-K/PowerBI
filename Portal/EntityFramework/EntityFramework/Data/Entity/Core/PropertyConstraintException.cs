using System;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002DE RID: 734
	[Serializable]
	public sealed class PropertyConstraintException : ConstraintException
	{
		// Token: 0x0600233A RID: 9018 RVA: 0x000636CE File Offset: 0x000618CE
		public PropertyConstraintException()
		{
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000636D6 File Offset: 0x000618D6
		public PropertyConstraintException(string message)
			: base(message)
		{
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000636DF File Offset: 0x000618DF
		public PropertyConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000636E9 File Offset: 0x000618E9
		public PropertyConstraintException(string message, string propertyName)
			: base(message)
		{
			Check.NotEmpty(propertyName, "propertyName");
			this.PropertyName = propertyName;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x00063705 File Offset: 0x00061905
		public PropertyConstraintException(string message, string propertyName, Exception innerException)
			: base(message, innerException)
		{
			Check.NotEmpty(propertyName, "propertyName");
			this.PropertyName = propertyName;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x00063722 File Offset: 0x00061922
		private PropertyConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.PropertyName = info.GetString("PropertyName");
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0006373D File Offset: 0x0006193D
		public string PropertyName { get; }

		// Token: 0x06002341 RID: 9025 RVA: 0x00063745 File Offset: 0x00061945
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("PropertyName", this.PropertyName);
		}
	}
}
