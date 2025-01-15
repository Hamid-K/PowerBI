using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002153 RID: 8531
	internal class ExtendedFilePropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D3FA RID: 54266 RVA: 0x002A3200 File Offset: 0x002A1400
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ExtendedFilePropertiesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ExtendedFilePropertiesPart._partConstraint = dictionary;
			}
			return ExtendedFilePropertiesPart._partConstraint;
		}

		// Token: 0x0600D3FB RID: 54267 RVA: 0x002A3228 File Offset: 0x002A1428
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ExtendedFilePropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ExtendedFilePropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return ExtendedFilePropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D3FC RID: 54268 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ExtendedFilePropertiesPart()
		{
		}

		// Token: 0x1700332C RID: 13100
		// (get) Token: 0x0600D3FD RID: 54269 RVA: 0x002A324D File Offset: 0x002A144D
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties";
			}
		}

		// Token: 0x1700332D RID: 13101
		// (get) Token: 0x0600D3FE RID: 54270 RVA: 0x002A3254 File Offset: 0x002A1454
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.extended-properties+xml";
			}
		}

		// Token: 0x1700332E RID: 13102
		// (get) Token: 0x0600D3FF RID: 54271 RVA: 0x002A31EF File Offset: 0x002A13EF
		internal sealed override string TargetPath
		{
			get
			{
				return "docProps";
			}
		}

		// Token: 0x1700332F RID: 13103
		// (get) Token: 0x0600D400 RID: 54272 RVA: 0x002A325B File Offset: 0x002A145B
		internal sealed override string TargetName
		{
			get
			{
				return "app";
			}
		}

		// Token: 0x17003330 RID: 13104
		// (get) Token: 0x0600D401 RID: 54273 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003331 RID: 13105
		// (get) Token: 0x0600D402 RID: 54274 RVA: 0x002A3262 File Offset: 0x002A1462
		// (set) Token: 0x0600D403 RID: 54275 RVA: 0x002A326A File Offset: 0x002A146A
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Properties;
			}
		}

		// Token: 0x17003332 RID: 13106
		// (get) Token: 0x0600D404 RID: 54276 RVA: 0x002A3278 File Offset: 0x002A1478
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x17003333 RID: 13107
		// (get) Token: 0x0600D405 RID: 54277 RVA: 0x002A3280 File Offset: 0x002A1480
		// (set) Token: 0x0600D406 RID: 54278 RVA: 0x002A3296 File Offset: 0x002A1496
		public Properties Properties
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Properties>();
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

		// Token: 0x040069BD RID: 27069
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties";

		// Token: 0x040069BE RID: 27070
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.extended-properties+xml";

		// Token: 0x040069BF RID: 27071
		internal const string TargetPathConstant = "docProps";

		// Token: 0x040069C0 RID: 27072
		internal const string TargetNameConstant = "app";

		// Token: 0x040069C1 RID: 27073
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069C2 RID: 27074
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x040069C3 RID: 27075
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Properties _rootEle;
	}
}
