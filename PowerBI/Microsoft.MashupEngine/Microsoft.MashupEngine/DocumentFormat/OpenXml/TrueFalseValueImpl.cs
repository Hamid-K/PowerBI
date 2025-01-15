using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200213F RID: 8511
	internal class TrueFalseValueImpl : OpenXmlSimpleType
	{
		// Token: 0x0600D354 RID: 54100 RVA: 0x0029F2AC File Offset: 0x0029D4AC
		public TrueFalseValueImpl(Func<string, bool> getBooleanValueMethod, Func<bool, string> getDefaultTextMethod)
		{
			this._getBooleanValueMethod = getBooleanValueMethod;
			this._getDefaultTextValueMethod = getDefaultTextMethod;
		}

		// Token: 0x17003309 RID: 13065
		// (get) Token: 0x0600D355 RID: 54101 RVA: 0x0029F2C2 File Offset: 0x0029D4C2
		// (set) Token: 0x0600D356 RID: 54102 RVA: 0x0029F2FB File Offset: 0x0029D4FB
		public override string InnerText
		{
			get
			{
				if (base.TextValue == null && this._innerValue != null)
				{
					base.TextValue = this._getDefaultTextValueMethod(this._innerValue.Value);
				}
				return base.TextValue;
			}
			set
			{
				base.TextValue = value;
				this._innerValue = null;
			}
		}

		// Token: 0x1700330A RID: 13066
		// (get) Token: 0x0600D357 RID: 54103 RVA: 0x0029F310 File Offset: 0x0029D510
		public override bool HasValue
		{
			get
			{
				if (this._innerValue == null && base.TextValue != null)
				{
					this.TryParse();
				}
				return this._innerValue != null;
			}
		}

		// Token: 0x1700330B RID: 13067
		// (get) Token: 0x0600D358 RID: 54104 RVA: 0x0029F339 File Offset: 0x0029D539
		// (set) Token: 0x0600D359 RID: 54105 RVA: 0x0029F361 File Offset: 0x0029D561
		public bool Value
		{
			get
			{
				if (this._innerValue == null && base.TextValue != null)
				{
					this.Parse();
				}
				return this._innerValue.Value;
			}
			set
			{
				this._innerValue = new bool?(value);
				base.TextValue = null;
			}
		}

		// Token: 0x0600D35A RID: 54106 RVA: 0x0029F376 File Offset: 0x0029D576
		internal override void Parse()
		{
			this._innerValue = new bool?(this._getBooleanValueMethod(base.TextValue));
		}

		// Token: 0x0600D35B RID: 54107 RVA: 0x0029F394 File Offset: 0x0029D594
		internal override bool TryParse()
		{
			this._innerValue = null;
			bool flag2;
			try
			{
				bool flag = this._getBooleanValueMethod(base.TextValue);
				this._innerValue = new bool?(flag);
				flag2 = true;
			}
			catch (FormatException)
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600D35C RID: 54108 RVA: 0x000091AE File Offset: 0x000073AE
		internal override OpenXmlSimpleType CloneImp()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04006995 RID: 27029
		private Func<string, bool> _getBooleanValueMethod;

		// Token: 0x04006996 RID: 27030
		private Func<bool, string> _getDefaultTextValueMethod;

		// Token: 0x04006997 RID: 27031
		private bool? _innerValue;
	}
}
