using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000131 RID: 305
	internal sealed class CreateFolderActionParameters : CreateItemActionParameters
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002E0A6 File Offset: 0x0002C2A6
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0002E0AE File Offset: 0x0002C2AE
		public string SubType
		{
			get
			{
				return this.m_subType;
			}
			set
			{
				this.m_subType = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0002E0B7 File Offset: 0x0002C2B7
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", base.ItemName, base.ParentPath);
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002E0D4 File Offset: 0x0002C2D4
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Folder");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			Match match = base.ForbiddenCharsCombinations.Match(base.ItemName);
			string text = HttpUtility.UrlDecode(base.ItemName);
			if (match.Success)
			{
				throw new InvalidItemNameException(string.Format("Name contains invalid character '{0}'", match.ToString()), CatalogItemNameUtility.MaxItemNameLength);
			}
			if (text.Length != base.ItemName.Length)
			{
				throw new InvalidItemNameException("Name contains URL-encoded characters", CatalogItemNameUtility.MaxItemNameLength);
			}
		}

		// Token: 0x04000502 RID: 1282
		private string m_subType;
	}
}
