using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office.Word;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002159 RID: 8537
	internal class CustomizationPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D471 RID: 54385 RVA: 0x002A4040 File Offset: 0x002A2240
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomizationPart._partConstraint == null)
			{
				CustomizationPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.microsoft.com/office/2006/relationships/attachedToolbars",
					new PartConstraintRule("WordAttachedToolbarsPart", "application/vnd.ms-word.attachedToolbars", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return CustomizationPart._partConstraint;
		}

		// Token: 0x0600D472 RID: 54386 RVA: 0x002A4084 File Offset: 0x002A2284
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomizationPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomizationPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomizationPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D473 RID: 54387 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomizationPart()
		{
		}

		// Token: 0x0600D474 RID: 54388 RVA: 0x002A40AC File Offset: 0x002A22AC
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars")
			{
				return new WordAttachedToolbarsPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x17003371 RID: 13169
		// (get) Token: 0x0600D475 RID: 54389 RVA: 0x002A40EF File Offset: 0x002A22EF
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/keyMapCustomizations";
			}
		}

		// Token: 0x17003372 RID: 13170
		// (get) Token: 0x0600D476 RID: 54390 RVA: 0x002A40F6 File Offset: 0x002A22F6
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-word.keyMapCustomizations+xml";
			}
		}

		// Token: 0x17003373 RID: 13171
		// (get) Token: 0x0600D477 RID: 54391 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003374 RID: 13172
		// (get) Token: 0x0600D478 RID: 54392 RVA: 0x002A4104 File Offset: 0x002A2304
		internal sealed override string TargetName
		{
			get
			{
				return "customizations";
			}
		}

		// Token: 0x17003375 RID: 13173
		// (get) Token: 0x0600D479 RID: 54393 RVA: 0x002A410B File Offset: 0x002A230B
		public WordAttachedToolbarsPart WordAttachedToolbarsPart
		{
			get
			{
				return base.GetSubPartOfType<WordAttachedToolbarsPart>();
			}
		}

		// Token: 0x17003376 RID: 13174
		// (get) Token: 0x0600D47A RID: 54394 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003377 RID: 13175
		// (get) Token: 0x0600D47B RID: 54395 RVA: 0x002A4113 File Offset: 0x002A2313
		// (set) Token: 0x0600D47C RID: 54396 RVA: 0x002A411B File Offset: 0x002A231B
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as TemplateCommandGroup;
			}
		}

		// Token: 0x17003378 RID: 13176
		// (get) Token: 0x0600D47D RID: 54397 RVA: 0x002A4129 File Offset: 0x002A2329
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.TemplateCommandGroup;
			}
		}

		// Token: 0x17003379 RID: 13177
		// (get) Token: 0x0600D47E RID: 54398 RVA: 0x002A4131 File Offset: 0x002A2331
		// (set) Token: 0x0600D47F RID: 54399 RVA: 0x002A3296 File Offset: 0x002A1496
		public TemplateCommandGroup TemplateCommandGroup
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<TemplateCommandGroup>();
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

		// Token: 0x040069E4 RID: 27108
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/keyMapCustomizations";

		// Token: 0x040069E5 RID: 27109
		internal const string ContentTypeConstant = "application/vnd.ms-word.keyMapCustomizations+xml";

		// Token: 0x040069E6 RID: 27110
		internal const string TargetPathConstant = ".";

		// Token: 0x040069E7 RID: 27111
		internal const string TargetNameConstant = "customizations";

		// Token: 0x040069E8 RID: 27112
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069E9 RID: 27113
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x040069EA RID: 27114
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TemplateCommandGroup _rootEle;
	}
}
