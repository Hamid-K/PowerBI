using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200217B RID: 8571
	internal class WorkbookUserDataPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D727 RID: 55079 RVA: 0x002A86AC File Offset: 0x002A68AC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorkbookUserDataPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookUserDataPart._partConstraint = dictionary;
			}
			return WorkbookUserDataPart._partConstraint;
		}

		// Token: 0x0600D728 RID: 55080 RVA: 0x002A86D4 File Offset: 0x002A68D4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorkbookUserDataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookUserDataPart._dataPartReferenceConstraint = dictionary;
			}
			return WorkbookUserDataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D729 RID: 55081 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorkbookUserDataPart()
		{
		}

		// Token: 0x170034EF RID: 13551
		// (get) Token: 0x0600D72A RID: 55082 RVA: 0x002A86F9 File Offset: 0x002A68F9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/usernames";
			}
		}

		// Token: 0x170034F0 RID: 13552
		// (get) Token: 0x0600D72B RID: 55083 RVA: 0x002A8700 File Offset: 0x002A6900
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.userNames+xml";
			}
		}

		// Token: 0x170034F1 RID: 13553
		// (get) Token: 0x0600D72C RID: 55084 RVA: 0x002A85C9 File Offset: 0x002A67C9
		internal sealed override string TargetPath
		{
			get
			{
				return "revisions";
			}
		}

		// Token: 0x170034F2 RID: 13554
		// (get) Token: 0x0600D72D RID: 55085 RVA: 0x002A8707 File Offset: 0x002A6907
		internal sealed override string TargetName
		{
			get
			{
				return "userNames";
			}
		}

		// Token: 0x170034F3 RID: 13555
		// (get) Token: 0x0600D72E RID: 55086 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034F4 RID: 13556
		// (get) Token: 0x0600D72F RID: 55087 RVA: 0x002A870E File Offset: 0x002A690E
		// (set) Token: 0x0600D730 RID: 55088 RVA: 0x002A8716 File Offset: 0x002A6916
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Users;
			}
		}

		// Token: 0x170034F5 RID: 13557
		// (get) Token: 0x0600D731 RID: 55089 RVA: 0x002A8724 File Offset: 0x002A6924
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Users;
			}
		}

		// Token: 0x170034F6 RID: 13558
		// (get) Token: 0x0600D732 RID: 55090 RVA: 0x002A872C File Offset: 0x002A692C
		// (set) Token: 0x0600D733 RID: 55091 RVA: 0x002A3296 File Offset: 0x002A1496
		public Users Users
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Users>();
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

		// Token: 0x04006AC7 RID: 27335
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/usernames";

		// Token: 0x04006AC8 RID: 27336
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.userNames+xml";

		// Token: 0x04006AC9 RID: 27337
		internal const string TargetPathConstant = "revisions";

		// Token: 0x04006ACA RID: 27338
		internal const string TargetNameConstant = "userNames";

		// Token: 0x04006ACB RID: 27339
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006ACC RID: 27340
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006ACD RID: 27341
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Users _rootEle;
	}
}
