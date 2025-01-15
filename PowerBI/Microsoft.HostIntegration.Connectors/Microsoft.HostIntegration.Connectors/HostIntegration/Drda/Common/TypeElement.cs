using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000794 RID: 1940
	public class TypeElement
	{
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000D1D23 File Offset: 0x000CFF23
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x000D1D2B File Offset: 0x000CFF2B
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000D1D34 File Offset: 0x000CFF34
		// (set) Token: 0x06003E96 RID: 16022 RVA: 0x000D1D3C File Offset: 0x000CFF3C
		public Dictionary<string, string> Properties
		{
			get
			{
				return this._properties;
			}
			set
			{
				this._properties = value;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		public string this[string key]
		{
			get
			{
				return this._properties[key];
			}
		}

		// Token: 0x04002525 RID: 9509
		private string _type;

		// Token: 0x04002526 RID: 9510
		private Dictionary<string, string> _properties = new Dictionary<string, string>();
	}
}
