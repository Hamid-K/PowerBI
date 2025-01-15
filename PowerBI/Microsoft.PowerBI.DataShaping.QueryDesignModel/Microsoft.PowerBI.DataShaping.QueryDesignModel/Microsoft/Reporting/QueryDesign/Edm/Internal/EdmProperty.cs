using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000210 RID: 528
	public abstract class EdmProperty : EdmMember, ISupportsReferenceName
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x00043508 File Offset: 0x00041708
		protected EdmProperty(EdmProperty edmProperty, ConceptualPrimitiveResultType conceptualType, StructuralType declaringType, XElement extensionElem)
			: base(declaringType, extensionElem)
		{
			this._edmProperty = ArgumentValidation.CheckNotNull<EdmProperty>(edmProperty, "edmProperty");
			this._alignment = extensionElem.GetEnumAttributeOrDefault(Extensions.AlignmentAttr, Alignment.Default);
			this._formatString = extensionElem.GetStringAttributeOrDefault(Extensions.FormatStringAttr, null);
			this._referenceName = extensionElem.GetStringAttributeOrDefault(Extensions.ReferenceNameAttr, null);
			this._stability = extensionElem.GetEnumAttributeOrDefault(Extensions.StabilityAttr, Stability.Stable);
			this._displayFolderParents = new ObservableCollection<EdmDisplayFolder>();
			this._displayFolderParentsReadOnly = new ReadOnlyObservableCollection<EdmDisplayFolder>(this._displayFolderParents);
			this._defaultMember = this.GetDefaultMemberValue(extensionElem.GetElementOrNull(Extensions.DefaultMemberElem));
			this._statistics = EdmProperty.ParseStatistics(extensionElem.GetElementOrNull(Extensions.StatisticsElem));
			this._conceptualType = conceptualType;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x000435D0 File Offset: 0x000417D0
		internal virtual ActualDataType? ActualType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x000435E6 File Offset: 0x000417E6
		public Alignment Alignment
		{
			get
			{
				return this._alignment;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x000435EE File Offset: 0x000417EE
		public ScalarValue? DefaultMember
		{
			get
			{
				return this._defaultMember;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x000435F6 File Offset: 0x000417F6
		public string FormatString
		{
			get
			{
				return this._formatString;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x00043600 File Offset: 0x00041800
		public bool HasVariantDataType
		{
			get
			{
				ActualDataType? actualType = this.ActualType;
				ActualDataType actualDataType = ActualDataType.Any;
				return (actualType.GetValueOrDefault() == actualDataType) & (actualType != null);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00043628 File Offset: 0x00041828
		public string ReferenceName
		{
			get
			{
				return this._referenceName ?? base.Name;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0004363A File Offset: 0x0004183A
		public Stability Stability
		{
			get
			{
				return this._stability;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00043642 File Offset: 0x00041842
		internal EdmProperty InternalEdmProperty
		{
			get
			{
				return this._edmProperty;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0004364A File Offset: 0x0004184A
		internal sealed override EdmMember InternalEdmMember
		{
			get
			{
				return this._edmProperty;
			}
		}

		// Token: 0x060018A7 RID: 6311
		public abstract bool IsCompatible(AggregateFunction? agg);

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x00043652 File Offset: 0x00041852
		public virtual IEnumerable<EdmDisplayFolder> DisplayFolderParents
		{
			get
			{
				return this._displayFolderParentsReadOnly;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0004365A File Offset: 0x0004185A
		internal EdmPropertyStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x00043662 File Offset: 0x00041862
		internal ConceptualPrimitiveResultType ConceptualType
		{
			get
			{
				return this._conceptualType;
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0004366C File Offset: 0x0004186C
		private static EdmPropertyStatistics ParseStatistics(XElement statsElement)
		{
			int num;
			if (statsElement.TryGetInt32Attribute(Extensions.DistinctValueCountAttr, out num))
			{
				return new EdmPropertyStatistics(num);
			}
			return null;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00043690 File Offset: 0x00041890
		internal void AddDisplayFolderParent(EdmDisplayFolder displayFolder)
		{
			ArgumentValidation.CheckNotNull<EdmDisplayFolder>(displayFolder, "displayFolder");
			this._displayFolderParents.Add(displayFolder);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000436AC File Offset: 0x000418AC
		private ScalarValue? GetDefaultMemberValue(XElement parentElement)
		{
			if (parentElement == null)
			{
				return null;
			}
			if (parentElement.GetBooleanAttributeOrDefault(Extensions.NilAttr, false))
			{
				return new ScalarValue?(ScalarValue.Null);
			}
			return this.ConvertDefaultMemberValue(parentElement.Value);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000436EC File Offset: 0x000418EC
		private ScalarValue? ConvertDefaultMemberValue(string value)
		{
			ScalarValue scalarValue;
			if (PrimitiveType.Get((PrimitiveType)this._edmProperty.TypeUsage.EdmType).TryParse(value, out scalarValue))
			{
				return new ScalarValue?(scalarValue);
			}
			return null;
		}

		// Token: 0x04000D18 RID: 3352
		private readonly EdmProperty _edmProperty;

		// Token: 0x04000D19 RID: 3353
		private readonly Alignment _alignment;

		// Token: 0x04000D1A RID: 3354
		private readonly ScalarValue? _defaultMember;

		// Token: 0x04000D1B RID: 3355
		private readonly string _formatString;

		// Token: 0x04000D1C RID: 3356
		private readonly string _referenceName;

		// Token: 0x04000D1D RID: 3357
		private readonly Stability _stability;

		// Token: 0x04000D1E RID: 3358
		private readonly EdmPropertyStatistics _statistics;

		// Token: 0x04000D1F RID: 3359
		private readonly ConceptualPrimitiveResultType _conceptualType;

		// Token: 0x04000D20 RID: 3360
		private readonly ObservableCollection<EdmDisplayFolder> _displayFolderParents;

		// Token: 0x04000D21 RID: 3361
		private readonly ReadOnlyObservableCollection<EdmDisplayFolder> _displayFolderParentsReadOnly;
	}
}
