using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002166 RID: 8550
	internal class StyleDefinitionsPart : StylesPart, IFixedContentTypePart
	{
		// Token: 0x0600D5D9 RID: 54745 RVA: 0x002A6B44 File Offset: 0x002A4D44
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (StyleDefinitionsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				StyleDefinitionsPart._partConstraint = dictionary;
			}
			return StyleDefinitionsPart._partConstraint;
		}

		// Token: 0x0600D5DA RID: 54746 RVA: 0x002A6B6C File Offset: 0x002A4D6C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (StyleDefinitionsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				StyleDefinitionsPart._dataPartReferenceConstraint = dictionary;
			}
			return StyleDefinitionsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D5DB RID: 54747 RVA: 0x002A6B91 File Offset: 0x002A4D91
		protected internal StyleDefinitionsPart()
		{
		}

		// Token: 0x17003424 RID: 13348
		// (get) Token: 0x0600D5DC RID: 54748 RVA: 0x002A6B99 File Offset: 0x002A4D99
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles";
			}
		}

		// Token: 0x17003425 RID: 13349
		// (get) Token: 0x0600D5DD RID: 54749 RVA: 0x002A6BA0 File Offset: 0x002A4DA0
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml";
			}
		}

		// Token: 0x17003426 RID: 13350
		// (get) Token: 0x0600D5DE RID: 54750 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003427 RID: 13351
		// (get) Token: 0x0600D5DF RID: 54751 RVA: 0x002A6BA7 File Offset: 0x002A4DA7
		internal sealed override string TargetName
		{
			get
			{
				return "styles";
			}
		}

		// Token: 0x17003428 RID: 13352
		// (get) Token: 0x0600D5E0 RID: 54752 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006A38 RID: 27192
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles";

		// Token: 0x04006A39 RID: 27193
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml";

		// Token: 0x04006A3A RID: 27194
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A3B RID: 27195
		internal const string TargetNameConstant = "styles";

		// Token: 0x04006A3C RID: 27196
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A3D RID: 27197
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
