using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003118 RID: 12568
	internal class ExpectedChildren
	{
		// Token: 0x0601B412 RID: 111634 RVA: 0x000020FD File Offset: 0x000002FD
		internal ExpectedChildren()
		{
		}

		// Token: 0x0601B413 RID: 111635 RVA: 0x00373B2B File Offset: 0x00371D2B
		internal void Add(int elementTypeId)
		{
			if (this._elementTypeIds == null)
			{
				this._elementTypeIds = new List<int>();
			}
			this._elementTypeIds.Add(elementTypeId);
		}

		// Token: 0x0601B414 RID: 111636 RVA: 0x00373B4C File Offset: 0x00371D4C
		internal void Add(string namesapceForXsdany)
		{
			if (this._xsdanyNamespaces == null)
			{
				this._xsdanyNamespaces = new List<string>();
			}
			this._xsdanyNamespaces.Add(namesapceForXsdany);
		}

		// Token: 0x0601B415 RID: 111637 RVA: 0x00373B70 File Offset: 0x00371D70
		internal void Add(ExpectedChildren expectedChildren)
		{
			if (expectedChildren._elementTypeIds != null && expectedChildren._elementTypeIds.Count > 0)
			{
				if (this._elementTypeIds == null)
				{
					this._elementTypeIds = new List<int>();
				}
				foreach (int num in expectedChildren._elementTypeIds)
				{
					this._elementTypeIds.Add(num);
				}
			}
			if (expectedChildren._xsdanyNamespaces != null && expectedChildren._xsdanyNamespaces.Count > 0)
			{
				if (this._xsdanyNamespaces == null)
				{
					this._xsdanyNamespaces = new List<string>();
				}
				foreach (string text in expectedChildren._xsdanyNamespaces)
				{
					this._xsdanyNamespaces.Add(text);
				}
			}
		}

		// Token: 0x170098DD RID: 39133
		// (get) Token: 0x0601B416 RID: 111638 RVA: 0x00373C58 File Offset: 0x00371E58
		internal int Count
		{
			get
			{
				int num = 0;
				if (this._elementTypeIds != null)
				{
					num = this._elementTypeIds.Count;
				}
				if (this._xsdanyNamespaces != null)
				{
					num += this._xsdanyNamespaces.Count;
				}
				return num;
			}
		}

		// Token: 0x0601B417 RID: 111639 RVA: 0x00373C94 File Offset: 0x00371E94
		internal string GetExpectedChildrenMessage(OpenXmlElement parent)
		{
			if (this._elementTypeIds != null || this._xsdanyNamespaces != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = string.Empty;
				if (this._elementTypeIds != null)
				{
					foreach (OpenXmlElement openXmlElement in parent.CreateChildrenElementsByIds(this._elementTypeIds))
					{
						stringBuilder.Append(text);
						stringBuilder.Append(string.Format(CultureInfo.CurrentUICulture, ValidationResources.Fmt_ElementName, new object[] { openXmlElement.NamespaceUri, openXmlElement.LocalName }));
						text = ValidationResources.Fmt_ElementNameSeparator;
					}
				}
				if (this._xsdanyNamespaces != null)
				{
					foreach (string text2 in this._xsdanyNamespaces)
					{
						stringBuilder.Append(text);
						stringBuilder.Append(string.Format(CultureInfo.CurrentUICulture, ValidationResources.Fmt_AnyElementInNamespace, new object[] { text2 }));
					}
				}
				return string.Format(CultureInfo.CurrentUICulture, ValidationResources.Fmt_ListOfPossibleElements, new object[] { stringBuilder.ToString() });
			}
			return string.Empty;
		}

		// Token: 0x0601B418 RID: 111640 RVA: 0x00373DEC File Offset: 0x00371FEC
		internal void Clear()
		{
			if (this._elementTypeIds != null)
			{
				this._elementTypeIds.Clear();
			}
			if (this._xsdanyNamespaces != null)
			{
				this._xsdanyNamespaces.Clear();
			}
		}

		// Token: 0x0400B4C4 RID: 46276
		private ICollection<int> _elementTypeIds;

		// Token: 0x0400B4C5 RID: 46277
		private ICollection<string> _xsdanyNamespaces;
	}
}
