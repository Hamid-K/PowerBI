using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002165 RID: 8549
	internal abstract class StylesPart : OpenXmlPart
	{
		// Token: 0x17003421 RID: 13345
		// (get) Token: 0x0600D5D3 RID: 54739 RVA: 0x002A6B0E File Offset: 0x002A4D0E
		// (set) Token: 0x0600D5D4 RID: 54740 RVA: 0x002A6B16 File Offset: 0x002A4D16
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Styles;
			}
		}

		// Token: 0x17003422 RID: 13346
		// (get) Token: 0x0600D5D5 RID: 54741 RVA: 0x002A6B24 File Offset: 0x002A4D24
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Styles;
			}
		}

		// Token: 0x17003423 RID: 13347
		// (get) Token: 0x0600D5D6 RID: 54742 RVA: 0x002A6B2C File Offset: 0x002A4D2C
		// (set) Token: 0x0600D5D7 RID: 54743 RVA: 0x002A3296 File Offset: 0x002A1496
		public Styles Styles
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Styles>();
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

		// Token: 0x04006A37 RID: 27191
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Styles _rootEle;
	}
}
