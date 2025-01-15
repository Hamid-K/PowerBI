using System;
using System.Globalization;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000316 RID: 790
	internal class SchemaEnumMember : SchemaElement
	{
		// Token: 0x060025B4 RID: 9652 RVA: 0x0006B8E8 File Offset: 0x00069AE8
		public SchemaEnumMember(SchemaElement parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x0006B8F2 File Offset: 0x00069AF2
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x0006B8FA File Offset: 0x00069AFA
		public long? Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0006B904 File Offset: 0x00069B04
		protected override bool HandleAttribute(XmlReader reader)
		{
			bool flag = base.HandleAttribute(reader);
			if (!flag && (flag = SchemaElement.CanHandleAttribute(reader, "Value")))
			{
				this.HandleValueAttribute(reader);
			}
			return flag;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0006B934 File Offset: 0x00069B34
		private void HandleValueAttribute(XmlReader reader)
		{
			long num;
			if (long.TryParse(reader.Value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out num))
			{
				this._value = new long?(num);
			}
		}

		// Token: 0x04000D41 RID: 3393
		private long? _value;
	}
}
