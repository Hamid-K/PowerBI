using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000343 RID: 835
	internal sealed class CompiledRichTextStyleInstance : StyleInstance, ICompiledStyleInstance
	{
		// Token: 0x06001FE8 RID: 8168 RVA: 0x0007AA2E File Offset: 0x00078C2E
		internal CompiledRichTextStyleInstance(IROMStyleDefinitionContainer styleDefinitionContainer, IReportScope reportScope, RenderingContext context)
			: base(styleDefinitionContainer, reportScope, context)
		{
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0007AA39 File Offset: 0x00078C39
		public override List<StyleAttributeNames> StyleAttributes
		{
			get
			{
				if (this.m_nonSharedStyles == null)
				{
					this.CompleteStyle();
				}
				return this.m_nonSharedStyles;
			}
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0007AA50 File Offset: 0x00078C50
		private void CompleteStyle()
		{
			List<StyleAttributeNames> styleAttributes = base.StyleAttributes;
			Dictionary<StyleAttributeNames, bool> dictionary = null;
			if (styleAttributes != null)
			{
				dictionary = new Dictionary<StyleAttributeNames, bool>(styleAttributes.Count);
				using (List<StyleAttributeNames>.Enumerator enumerator = styleAttributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StyleAttributeNames styleAttributeNames = enumerator.Current;
						dictionary[styleAttributeNames] = true;
					}
					goto IL_0055;
				}
			}
			this.m_nonSharedStyles = new List<StyleAttributeNames>();
			IL_0055:
			if (this.m_assignedValues != null)
			{
				foreach (KeyValuePair<StyleAttributeNames, bool> keyValuePair in this.m_assignedValues)
				{
					if (keyValuePair.Value)
					{
						StyleAttributeNames key = keyValuePair.Key;
						if (dictionary == null)
						{
							this.m_nonSharedStyles.Add(key);
						}
						else
						{
							dictionary[key] = true;
						}
					}
				}
			}
			if (dictionary != null)
			{
				this.m_nonSharedStyles = new List<StyleAttributeNames>(dictionary.Count);
				foreach (StyleAttributeNames styleAttributeNames2 in dictionary.Keys)
				{
					this.m_nonSharedStyles.Add(styleAttributeNames2);
				}
			}
		}

		// Token: 0x04000FF7 RID: 4087
		private List<StyleAttributeNames> m_nonSharedStyles;
	}
}
