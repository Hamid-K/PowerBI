using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200210E RID: 8462
	public class ElementEventArgs : EventArgs
	{
		// Token: 0x0600D111 RID: 53521 RVA: 0x00299EAB File Offset: 0x002980AB
		public ElementEventArgs(OpenXmlElement element, OpenXmlElement parentElement)
		{
			this._element = element;
			this._parentElement = parentElement;
		}

		// Token: 0x17003282 RID: 12930
		// (get) Token: 0x0600D112 RID: 53522 RVA: 0x00299EC1 File Offset: 0x002980C1
		public OpenXmlElement Element
		{
			get
			{
				return this._element;
			}
		}

		// Token: 0x17003283 RID: 12931
		// (get) Token: 0x0600D113 RID: 53523 RVA: 0x00299EC9 File Offset: 0x002980C9
		public OpenXmlElement ParentElement
		{
			get
			{
				return this._parentElement;
			}
		}

		// Token: 0x0400692F RID: 26927
		private OpenXmlElement _element;

		// Token: 0x04006930 RID: 26928
		private OpenXmlElement _parentElement;
	}
}
