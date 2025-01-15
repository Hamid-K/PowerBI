using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200003A RID: 58
	internal class SubtotalStyle2005 : Style2005
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00003E2A File Offset: 0x0000202A
		public SubtotalStyle2005()
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00003E3D File Offset: 0x0000203D
		public SubtotalStyle2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00003E51 File Offset: 0x00002051
		public override void Initialize()
		{
			this.m_initialize = true;
			base.Initialize();
			this.m_initialize = false;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00003E68 File Offset: 0x00002068
		internal override void OnSetObject(int propertyIndex)
		{
			base.OnSetObject(propertyIndex);
			Style.Definition.Properties properties = (Style.Definition.Properties)propertyIndex;
			string text = properties.ToString();
			if (this.m_initialize)
			{
				this.m_definedPropertiesOnInitialize[text] = true;
				return;
			}
			if (this.m_definedPropertiesOnInitialize.ContainsKey(text))
			{
				this.m_definedPropertiesOnInitialize.Remove(text);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00003EBD File Offset: 0x000020BD
		internal bool IsPropertyDefinedOnInitialize(string propertyName)
		{
			return this.m_definedPropertiesOnInitialize.ContainsKey(propertyName);
		}

		// Token: 0x04000031 RID: 49
		private bool m_initialize;

		// Token: 0x04000032 RID: 50
		private readonly Dictionary<string, bool> m_definedPropertiesOnInitialize = new Dictionary<string, bool>();
	}
}
