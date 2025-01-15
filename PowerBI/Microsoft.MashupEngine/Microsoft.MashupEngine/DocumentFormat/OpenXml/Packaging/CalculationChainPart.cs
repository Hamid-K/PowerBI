using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200216A RID: 8554
	internal class CalculationChainPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D629 RID: 54825 RVA: 0x002A7564 File Offset: 0x002A5764
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CalculationChainPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CalculationChainPart._partConstraint = dictionary;
			}
			return CalculationChainPart._partConstraint;
		}

		// Token: 0x0600D62A RID: 54826 RVA: 0x002A758C File Offset: 0x002A578C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CalculationChainPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CalculationChainPart._dataPartReferenceConstraint = dictionary;
			}
			return CalculationChainPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D62B RID: 54827 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CalculationChainPart()
		{
		}

		// Token: 0x17003457 RID: 13399
		// (get) Token: 0x0600D62C RID: 54828 RVA: 0x002A75B1 File Offset: 0x002A57B1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/calcChain";
			}
		}

		// Token: 0x17003458 RID: 13400
		// (get) Token: 0x0600D62D RID: 54829 RVA: 0x002A75B8 File Offset: 0x002A57B8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.calcChain+xml";
			}
		}

		// Token: 0x17003459 RID: 13401
		// (get) Token: 0x0600D62E RID: 54830 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700345A RID: 13402
		// (get) Token: 0x0600D62F RID: 54831 RVA: 0x002A75BF File Offset: 0x002A57BF
		internal sealed override string TargetName
		{
			get
			{
				return "calcChain";
			}
		}

		// Token: 0x1700345B RID: 13403
		// (get) Token: 0x0600D630 RID: 54832 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700345C RID: 13404
		// (get) Token: 0x0600D631 RID: 54833 RVA: 0x002A75C6 File Offset: 0x002A57C6
		// (set) Token: 0x0600D632 RID: 54834 RVA: 0x002A75CE File Offset: 0x002A57CE
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as CalculationChain;
			}
		}

		// Token: 0x1700345D RID: 13405
		// (get) Token: 0x0600D633 RID: 54835 RVA: 0x002A75DC File Offset: 0x002A57DC
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.CalculationChain;
			}
		}

		// Token: 0x1700345E RID: 13406
		// (get) Token: 0x0600D634 RID: 54836 RVA: 0x002A75E4 File Offset: 0x002A57E4
		// (set) Token: 0x0600D635 RID: 54837 RVA: 0x002A3296 File Offset: 0x002A1496
		public CalculationChain CalculationChain
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<CalculationChain>();
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

		// Token: 0x04006A51 RID: 27217
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/calcChain";

		// Token: 0x04006A52 RID: 27218
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.calcChain+xml";

		// Token: 0x04006A53 RID: 27219
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A54 RID: 27220
		internal const string TargetNameConstant = "calcChain";

		// Token: 0x04006A55 RID: 27221
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A56 RID: 27222
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A57 RID: 27223
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private CalculationChain _rootEle;
	}
}
