using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F9 RID: 505
	internal sealed class EdmFieldColorBy
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x00042380 File Offset: 0x00040580
		internal EdmFieldColorBy(EdmField parentField)
		{
			this._colorByField = EdmFieldColorBy.DetermineColorBy(parentField);
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00042394 File Offset: 0x00040594
		public EdmField ColorByField
		{
			get
			{
				return this._colorByField;
			}
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0004239C File Offset: 0x0004059C
		private static EdmField DetermineColorBy(EdmField parentField)
		{
			IList<EdmField> list = (parentField.DeclaringType as EntityType).Fields.Where(delegate(EdmField f)
			{
				if (f.Contents != null)
				{
					FieldContentType? contents = f.Contents;
					FieldContentType fieldContentType = FieldContentType.Color;
					if (!((contents.GetValueOrDefault() == fieldContentType) & (contents != null)))
					{
						return false;
					}
				}
				return string.Equals(f.CustomContents, "Color", StringComparison.OrdinalIgnoreCase);
			}).Evaluate<EdmField>();
			if (list.Count == 1)
			{
				return list[0];
			}
			string targetName = parentField.Name + "Color";
			return list.FirstOrDefault((EdmField f) => string.Equals(f.Name, targetName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x04000CDF RID: 3295
		private readonly EdmField _colorByField;
	}
}
