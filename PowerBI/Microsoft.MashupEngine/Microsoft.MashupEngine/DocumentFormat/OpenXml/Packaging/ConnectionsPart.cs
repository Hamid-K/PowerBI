using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200216D RID: 8557
	internal class ConnectionsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D659 RID: 54873 RVA: 0x002A78DC File Offset: 0x002A5ADC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ConnectionsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ConnectionsPart._partConstraint = dictionary;
			}
			return ConnectionsPart._partConstraint;
		}

		// Token: 0x0600D65A RID: 54874 RVA: 0x002A7904 File Offset: 0x002A5B04
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ConnectionsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ConnectionsPart._dataPartReferenceConstraint = dictionary;
			}
			return ConnectionsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D65B RID: 54875 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ConnectionsPart()
		{
		}

		// Token: 0x17003473 RID: 13427
		// (get) Token: 0x0600D65C RID: 54876 RVA: 0x002A7929 File Offset: 0x002A5B29
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/connections";
			}
		}

		// Token: 0x17003474 RID: 13428
		// (get) Token: 0x0600D65D RID: 54877 RVA: 0x002A7930 File Offset: 0x002A5B30
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.connections+xml";
			}
		}

		// Token: 0x17003475 RID: 13429
		// (get) Token: 0x0600D65E RID: 54878 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003476 RID: 13430
		// (get) Token: 0x0600D65F RID: 54879 RVA: 0x002A7937 File Offset: 0x002A5B37
		internal sealed override string TargetName
		{
			get
			{
				return "connections";
			}
		}

		// Token: 0x17003477 RID: 13431
		// (get) Token: 0x0600D660 RID: 54880 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003478 RID: 13432
		// (get) Token: 0x0600D661 RID: 54881 RVA: 0x002A793E File Offset: 0x002A5B3E
		// (set) Token: 0x0600D662 RID: 54882 RVA: 0x002A7946 File Offset: 0x002A5B46
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Connections;
			}
		}

		// Token: 0x17003479 RID: 13433
		// (get) Token: 0x0600D663 RID: 54883 RVA: 0x002A7954 File Offset: 0x002A5B54
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Connections;
			}
		}

		// Token: 0x1700347A RID: 13434
		// (get) Token: 0x0600D664 RID: 54884 RVA: 0x002A795C File Offset: 0x002A5B5C
		// (set) Token: 0x0600D665 RID: 54885 RVA: 0x002A3296 File Offset: 0x002A1496
		public Connections Connections
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Connections>();
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

		// Token: 0x04006A66 RID: 27238
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/connections";

		// Token: 0x04006A67 RID: 27239
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.connections+xml";

		// Token: 0x04006A68 RID: 27240
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A69 RID: 27241
		internal const string TargetNameConstant = "connections";

		// Token: 0x04006A6A RID: 27242
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A6B RID: 27243
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A6C RID: 27244
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Connections _rootEle;
	}
}
