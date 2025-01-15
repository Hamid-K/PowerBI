using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A7 RID: 8615
	internal abstract class CustomUIPart : OpenXmlPart
	{
		// Token: 0x170036C0 RID: 14016
		// (get) Token: 0x0600DA84 RID: 55940 RVA: 0x002ADBBF File Offset: 0x002ABDBF
		// (set) Token: 0x0600DA85 RID: 55941 RVA: 0x002ADBC7 File Offset: 0x002ABDC7
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as CustomUI;
			}
		}

		// Token: 0x170036C1 RID: 14017
		// (get) Token: 0x0600DA86 RID: 55942 RVA: 0x002ADBD5 File Offset: 0x002ABDD5
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.CustomUI;
			}
		}

		// Token: 0x170036C2 RID: 14018
		// (get) Token: 0x0600DA87 RID: 55943 RVA: 0x002ADBDD File Offset: 0x002ABDDD
		// (set) Token: 0x0600DA88 RID: 55944 RVA: 0x002A3296 File Offset: 0x002A1496
		public CustomUI CustomUI
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<CustomUI>();
				}
				return this._rootEle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x04006BF2 RID: 27634
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private CustomUI _rootEle;
	}
}
