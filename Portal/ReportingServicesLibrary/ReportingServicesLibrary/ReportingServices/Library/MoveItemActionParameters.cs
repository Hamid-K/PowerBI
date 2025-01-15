using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B1 RID: 177
	internal sealed class MoveItemActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00020E09 File Offset: 0x0001F009
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00020E11 File Offset: 0x0001F011
		public string SourceItemPath
		{
			get
			{
				return this.m_sourceItemPath;
			}
			set
			{
				this.m_sourceItemPath = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00020E1A File Offset: 0x0001F01A
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00020E22 File Offset: 0x0001F022
		public string TargetItemPath
		{
			get
			{
				return this.m_targetItemPath;
			}
			set
			{
				this.m_targetItemPath = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00020E2B File Offset: 0x0001F02B
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.SourceItemPath, this.TargetItemPath);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00020E48 File Offset: 0x0001F048
		internal override void Validate()
		{
			if (this.SourceItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
			if (this.TargetItemPath == null)
			{
				throw new MissingParameterException("Target");
			}
			foreach (string text in this.TargetItemPath.TrimStart(new char[] { '/' }).Split(new char[] { '/' }))
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new InvalidItemNameException("Name contains invalid character '/'", CatalogItemNameUtility.MaxItemNameLength);
				}
				Match match = base.ForbiddenCharsCombinations.Match(text);
				string text2 = HttpUtility.UrlDecode(text);
				if (match.Success)
				{
					throw new InvalidItemNameException(string.Format("Name contains invalid character '{0}'", match.ToString()), CatalogItemNameUtility.MaxItemNameLength);
				}
				if (text2.Length != text.Length)
				{
					throw new InvalidItemNameException("Name contains URL-encoded characters", CatalogItemNameUtility.MaxItemNameLength);
				}
			}
		}

		// Token: 0x04000410 RID: 1040
		private string m_sourceItemPath;

		// Token: 0x04000411 RID: 1041
		private string m_targetItemPath;
	}
}
