using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200040E RID: 1038
	[Serializable]
	internal abstract class SignatureStatementBase : TSqlStatement
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x0016E4B9 File Offset: 0x0016C6B9
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x0016E4C1 File Offset: 0x0016C6C1
		public bool IsCounter
		{
			get
			{
				return this._isCounter;
			}
			set
			{
				this._isCounter = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x0016E4CA File Offset: 0x0016C6CA
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x0016E4D2 File Offset: 0x0016C6D2
		public SignableElementKind ElementKind
		{
			get
			{
				return this._elementKind;
			}
			set
			{
				this._elementKind = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x0016E4DB File Offset: 0x0016C6DB
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x0016E4E3 File Offset: 0x0016C6E3
		public SchemaObjectName Element
		{
			get
			{
				return this._element;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._element = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x0016E4F3 File Offset: 0x0016C6F3
		public IList<CryptoMechanism> Cryptos
		{
			get
			{
				return this._cryptos;
			}
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x0016E4FC File Offset: 0x0016C6FC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Element != null)
			{
				this.Element.Accept(visitor);
			}
			int i = 0;
			int count = this.Cryptos.Count;
			while (i < count)
			{
				this.Cryptos[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E27 RID: 7719
		private bool _isCounter;

		// Token: 0x04001E28 RID: 7720
		private SignableElementKind _elementKind;

		// Token: 0x04001E29 RID: 7721
		private SchemaObjectName _element;

		// Token: 0x04001E2A RID: 7722
		private List<CryptoMechanism> _cryptos = new List<CryptoMechanism>();
	}
}
