using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002111 RID: 8465
	internal class OpenXmlChildElements : OpenXmlElementList
	{
		// Token: 0x0600D125 RID: 53541 RVA: 0x0029A100 File Offset: 0x00298300
		public OpenXmlChildElements(OpenXmlElement container)
		{
			this._container = container;
		}

		// Token: 0x0600D126 RID: 53542 RVA: 0x0029A110 File Offset: 0x00298310
		public override IEnumerator<OpenXmlElement> GetEnumerator()
		{
			if (this._container.HasChildren && this._container.FirstChild != null)
			{
				for (OpenXmlElement element = this._container.FirstChild; element != null; element = element.NextSibling())
				{
					yield return element;
				}
			}
			yield break;
		}

		// Token: 0x0600D127 RID: 53543 RVA: 0x0029A12C File Offset: 0x0029832C
		public override OpenXmlElement GetItem(int index)
		{
			if (this._container.HasChildren)
			{
				for (OpenXmlElement openXmlElement = this._container.FirstChild; openXmlElement != null; openXmlElement = openXmlElement.NextSibling())
				{
					if (index == 0)
					{
						return openXmlElement;
					}
					index--;
				}
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x17003288 RID: 12936
		// (get) Token: 0x0600D128 RID: 53544 RVA: 0x0029A174 File Offset: 0x00298374
		public override int Count
		{
			get
			{
				int num = 0;
				if (this._container.HasChildren)
				{
					for (OpenXmlElement openXmlElement = this._container.FirstChild; openXmlElement != null; openXmlElement = openXmlElement.NextSibling())
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x04006937 RID: 26935
		private OpenXmlElement _container;
	}
}
