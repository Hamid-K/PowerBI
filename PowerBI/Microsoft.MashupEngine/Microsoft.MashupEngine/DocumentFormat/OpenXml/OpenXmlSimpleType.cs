using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002128 RID: 8488
	public abstract class OpenXmlSimpleType : ICloneable
	{
		// Token: 0x0600D257 RID: 53847 RVA: 0x000020FD File Offset: 0x000002FD
		protected OpenXmlSimpleType()
		{
		}

		// Token: 0x0600D258 RID: 53848 RVA: 0x0029D6D5 File Offset: 0x0029B8D5
		protected OpenXmlSimpleType(OpenXmlSimpleType source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.TextValue = source.TextValue;
		}

		// Token: 0x170032E8 RID: 13032
		// (get) Token: 0x0600D259 RID: 53849 RVA: 0x0029D6F7 File Offset: 0x0029B8F7
		// (set) Token: 0x0600D25A RID: 53850 RVA: 0x0029D6FF File Offset: 0x0029B8FF
		protected string TextValue
		{
			get
			{
				return this._textValue;
			}
			set
			{
				this._textValue = value;
			}
		}

		// Token: 0x0600D25B RID: 53851 RVA: 0x0000336E File Offset: 0x0000156E
		internal virtual void Parse()
		{
		}

		// Token: 0x0600D25C RID: 53852 RVA: 0x00002139 File Offset: 0x00000339
		internal virtual bool TryParse()
		{
			return true;
		}

		// Token: 0x170032E9 RID: 13033
		// (get) Token: 0x0600D25D RID: 53853 RVA: 0x0029D708 File Offset: 0x0029B908
		public virtual bool HasValue
		{
			get
			{
				return this._textValue != null;
			}
		}

		// Token: 0x170032EA RID: 13034
		// (get) Token: 0x0600D25E RID: 53854 RVA: 0x0029D6F7 File Offset: 0x0029B8F7
		// (set) Token: 0x0600D25F RID: 53855 RVA: 0x0029D6FF File Offset: 0x0029B8FF
		public virtual string InnerText
		{
			get
			{
				return this._textValue;
			}
			set
			{
				this._textValue = value;
			}
		}

		// Token: 0x0600D260 RID: 53856 RVA: 0x0029D716 File Offset: 0x0029B916
		public override string ToString()
		{
			return this.InnerText;
		}

		// Token: 0x0600D261 RID: 53857 RVA: 0x0029D71E File Offset: 0x0029B91E
		public object Clone()
		{
			return this.CloneImp();
		}

		// Token: 0x0600D262 RID: 53858
		internal abstract OpenXmlSimpleType CloneImp();

		// Token: 0x0600D263 RID: 53859 RVA: 0x0029D726 File Offset: 0x0029B926
		public static implicit operator string(OpenXmlSimpleType xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				return null;
			}
			return xmlAttribute.InnerText;
		}

		// Token: 0x0600D264 RID: 53860 RVA: 0x000091AE File Offset: 0x000073AE
		internal virtual IEnumerable<OpenXmlSimpleType> GetListItems()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600D265 RID: 53861 RVA: 0x000091AE File Offset: 0x000073AE
		internal virtual bool IsInVersion(FileFormatVersions fileFormat)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04006983 RID: 27011
		private string _textValue;
	}
}
