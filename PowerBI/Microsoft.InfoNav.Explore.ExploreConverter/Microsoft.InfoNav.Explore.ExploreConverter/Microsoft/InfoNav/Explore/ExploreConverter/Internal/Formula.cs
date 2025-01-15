using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000018 RID: 24
	[DataContract]
	internal sealed class Formula
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003A3E File Offset: 0x00001C3E
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003A46 File Offset: 0x00001C46
		[DataMember]
		public List<string> QualifiedName { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003A4F File Offset: 0x00001C4F
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003A57 File Offset: 0x00001C57
		[DataMember]
		public string Function { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003A60 File Offset: 0x00001C60
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003A68 File Offset: 0x00001C68
		[DataMember]
		public List<Formula> Arguments { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003A71 File Offset: 0x00001C71
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003A79 File Offset: 0x00001C79
		public FormulaEdmReferenceKind? EdmReferenceKind { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00003A84 File Offset: 0x00001C84
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Formula formula = obj as Formula;
			if (formula == null)
			{
				return false;
			}
			if (((this.QualifiedName == null && formula.QualifiedName == null) || (this.QualifiedName != null && formula.QualifiedName != null && this.QualifiedName.SequenceEqual(formula.QualifiedName))) && this.Function == formula.Function && ((this.Arguments == null && formula.Arguments == null) || (this.Arguments != null && formula.Arguments != null && this.Arguments.SequenceEqual(formula.Arguments))))
			{
				FormulaEdmReferenceKind? edmReferenceKind = this.EdmReferenceKind;
				FormulaEdmReferenceKind? edmReferenceKind2 = formula.EdmReferenceKind;
				return (edmReferenceKind.GetValueOrDefault() == edmReferenceKind2.GetValueOrDefault()) & (edmReferenceKind != null == (edmReferenceKind2 != null));
			}
			return false;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003B54 File Offset: 0x00001D54
		public override int GetHashCode()
		{
			return ((this.QualifiedName == null) ? base.GetHashCode() : this.QualifiedName.GetHashCode()) ^ ((this.Function == null) ? base.GetHashCode() : this.Function.GetHashCode()) ^ ((this.Arguments == null) ? base.GetHashCode() : this.Arguments.GetHashCode()) ^ ((this.EdmReferenceKind == null) ? base.GetHashCode() : this.EdmReferenceKind.GetHashCode());
		}
	}
}
