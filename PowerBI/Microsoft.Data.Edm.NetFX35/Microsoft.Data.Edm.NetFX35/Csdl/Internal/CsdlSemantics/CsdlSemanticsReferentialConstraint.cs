using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020001AB RID: 427
	internal class CsdlSemanticsReferentialConstraint : CsdlSemanticsElement, IEdmCheckable
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x00018A64 File Offset: 0x00016C64
		public CsdlSemanticsReferentialConstraint(CsdlSemanticsAssociation context, CsdlReferentialConstraint constraint)
			: base(constraint)
		{
			this.context = context;
			this.constraint = constraint;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00018ABD File Offset: 0x00016CBD
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00018ACA File Offset: 0x00016CCA
		public override CsdlElement Element
		{
			get
			{
				return this.constraint;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00018AD2 File Offset: 0x00016CD2
		public IEdmAssociationEnd PrincipalEnd
		{
			get
			{
				return this.principalCache.GetValue(this, CsdlSemanticsReferentialConstraint.ComputePrincipalFunc, null);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00018AE6 File Offset: 0x00016CE6
		public IEnumerable<IEdmStructuralProperty> DependentProperties
		{
			get
			{
				return this.dependentPropertiesCache.GetValue(this, CsdlSemanticsReferentialConstraint.ComputeDependentPropertiesFunc, null);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00018AFA File Offset: 0x00016CFA
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsReferentialConstraint.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00018B10 File Offset: 0x00016D10
		private IEdmAssociationEnd DependentEnd
		{
			get
			{
				IEdmAssociation declaringAssociation = this.PrincipalEnd.DeclaringAssociation;
				if (this.PrincipalEnd != declaringAssociation.End1)
				{
					return declaringAssociation.End1;
				}
				return declaringAssociation.End2;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00018B44 File Offset: 0x00016D44
		private IEnumerable<string> PrincipalKeyPropertiesNotFoundInPrincipalProperties
		{
			get
			{
				return this.principalKeyPropertiesNotFoundInPrincipalPropertiesCache.GetValue(this, CsdlSemanticsReferentialConstraint.ComputePrincipalKeyPropertiesNotFoundInPrincipalPropertiesFunc, null);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00018B58 File Offset: 0x00016D58
		private IEnumerable<string> DependentPropertiesNotFoundInDependentType
		{
			get
			{
				return this.dependentPropertiesNotFoundInDependentTypeCache.GetValue(this, CsdlSemanticsReferentialConstraint.ComputeDependentPropertiesNotFoundInDependentTypeFunc, null);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00018B6C File Offset: 0x00016D6C
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = null;
			IEdmEntityType entityType = this.PrincipalEnd.EntityType;
			CsdlReferentialConstraintRole principal = this.constraint.Principal;
			CsdlReferentialConstraintRole dependent = this.constraint.Dependent;
			if (this.constraint.Principal.Role == this.constraint.Dependent.Role)
			{
				list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(base.Location, EdmErrorCode.SameRoleReferredInReferentialConstraint, Strings.EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(this.constraint.Principal.Role)));
			}
			if (this.constraint.Dependent.Role != this.context.End1.Name && this.constraint.Dependent.Role != this.context.End2.Name)
			{
				list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(base.Location, EdmErrorCode.InvalidRoleInRelationshipConstraint, Strings.CsdlParser_InvalidEndRoleInRelationshipConstraint(this.constraint.Dependent.Role, this.context.Name)));
			}
			if (this.constraint.Principal.Role != this.context.End1.Name && this.constraint.Principal.Role != this.context.End2.Name)
			{
				list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(base.Location, EdmErrorCode.InvalidRoleInRelationshipConstraint, Strings.CsdlParser_InvalidEndRoleInRelationshipConstraint(this.constraint.Principal.Role, this.context.Name)));
			}
			if (list == null)
			{
				if (Enumerable.Count<CsdlPropertyReference>(principal.Properties) != Enumerable.Count<CsdlPropertyReference>(dependent.Properties))
				{
					list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(base.Location, EdmErrorCode.MismatchNumberOfPropertiesInRelationshipConstraint, Strings.EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint));
				}
				if (Enumerable.Count<IEdmStructuralProperty>(entityType.Key()) != Enumerable.Count<CsdlPropertyReference>(principal.Properties) || Enumerable.Count<string>(this.PrincipalKeyPropertiesNotFoundInPrincipalProperties) != 0)
				{
					string text = Strings.EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd(this.DependentEnd.DeclaringAssociation.Namespace + '.' + this.DependentEnd.DeclaringAssociation.Name, this.constraint.Principal.Role);
					list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(base.Location, EdmErrorCode.BadPrincipalPropertiesInReferentialConstraint, text));
				}
				foreach (string text2 in this.DependentPropertiesNotFoundInDependentType)
				{
					string text3 = Strings.EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd(text2, this.constraint.Dependent.Role);
					list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(base.Location, EdmErrorCode.InvalidPropertyInRelationshipConstraint, text3));
				}
			}
			return list ?? Enumerable.Empty<EdmError>();
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00018E24 File Offset: 0x00017024
		private IEdmAssociationEnd ComputePrincipal()
		{
			IEdmAssociationEnd edmAssociationEnd = this.context.End1;
			if (edmAssociationEnd.Name != this.constraint.Principal.Role)
			{
				edmAssociationEnd = this.context.End2;
			}
			if (edmAssociationEnd.Name != this.constraint.Principal.Role)
			{
				edmAssociationEnd = new BadAssociationEnd(this.context, this.constraint.Principal.Role, new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.BadNonComputableAssociationEnd, Strings.Bad_UncomputableAssociationEnd(this.constraint.Principal.Role))
				});
			}
			return edmAssociationEnd;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00018F04 File Offset: 0x00017104
		private IEnumerable<IEdmStructuralProperty> ComputeDependentProperties()
		{
			List<IEdmStructuralProperty> list = new List<IEdmStructuralProperty>();
			IEdmEntityType entityType = this.DependentEnd.EntityType;
			IEdmEntityType principalRoleType = this.PrincipalEnd.EntityType;
			CsdlReferentialConstraintRole principal = this.constraint.Principal;
			CsdlReferentialConstraintRole dependent = this.constraint.Dependent;
			if (Enumerable.Count<IEdmStructuralProperty>(principalRoleType.Key()) == Enumerable.Count<CsdlPropertyReference>(principal.Properties) && Enumerable.Count<CsdlPropertyReference>(principal.Properties) == Enumerable.Count<CsdlPropertyReference>(dependent.Properties) && Enumerable.Count<string>(this.PrincipalKeyPropertiesNotFoundInPrincipalProperties) == 0 && Enumerable.Count<string>(this.DependentPropertiesNotFoundInDependentType) == 0)
			{
				using (IEnumerator<IEdmStructuralProperty> enumerator = this.PrincipalEnd.EntityType.Key().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmStructuralProperty principalKeyProperty = enumerator.Current;
						CsdlPropertyReference csdlPropertyReference = Enumerable.FirstOrDefault<CsdlPropertyReference>(Enumerable.Where<CsdlPropertyReference>(principal.Properties, (CsdlPropertyReference reference) => principalRoleType.FindProperty(reference.PropertyName).Equals(principalKeyProperty)));
						int num = principal.IndexOf(csdlPropertyReference);
						CsdlPropertyReference csdlPropertyReference2 = Enumerable.ElementAt<CsdlPropertyReference>(dependent.Properties, num);
						IEdmStructuralProperty edmStructuralProperty = entityType.FindProperty(csdlPropertyReference2.PropertyName) as IEdmStructuralProperty;
						list.Add(edmStructuralProperty);
					}
					return list;
				}
			}
			list = new List<IEdmStructuralProperty>();
			foreach (CsdlPropertyReference csdlPropertyReference3 in dependent.Properties)
			{
				list.Add(new BadProperty(entityType, csdlPropertyReference3.PropertyName, new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.TypeMismatchRelationshipConstraint, Strings.CsdlSemantics_ReferentialConstraintMismatch)
				}));
			}
			return list;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00019108 File Offset: 0x00017308
		private IEnumerable<string> ComputePrincipalKeyPropertiesNotFoundInPrincipalProperties()
		{
			List<string> list = null;
			using (IEnumerator<IEdmStructuralProperty> enumerator = this.PrincipalEnd.EntityType.Key().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEdmStructuralProperty principalKeyProperty = enumerator.Current;
					CsdlReferentialConstraintRole principal = this.constraint.Principal;
					if (Enumerable.FirstOrDefault<CsdlPropertyReference>(Enumerable.Where<CsdlPropertyReference>(principal.Properties, (CsdlPropertyReference reference) => reference.PropertyName == principalKeyProperty.Name)) == null)
					{
						list = CsdlSemanticsElement.AllocateAndAdd<string>(list, principalKeyProperty.Name);
					}
				}
			}
			return list ?? Enumerable.Empty<string>();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000191C0 File Offset: 0x000173C0
		private IEnumerable<string> ComputeDependentPropertiesNotFoundInDependentType()
		{
			List<string> list = new List<string>();
			IEdmEntityType entityType = this.DependentEnd.EntityType;
			foreach (CsdlPropertyReference csdlPropertyReference in this.constraint.Dependent.Properties)
			{
				if (entityType.FindProperty(csdlPropertyReference.PropertyName) == null)
				{
					list = CsdlSemanticsElement.AllocateAndAdd<string>(list, csdlPropertyReference.PropertyName);
				}
			}
			return list ?? Enumerable.Empty<string>();
		}

		// Token: 0x0400047D RID: 1149
		private readonly CsdlSemanticsAssociation context;

		// Token: 0x0400047E RID: 1150
		private readonly CsdlReferentialConstraint constraint;

		// Token: 0x0400047F RID: 1151
		private readonly Cache<CsdlSemanticsReferentialConstraint, IEdmAssociationEnd> principalCache = new Cache<CsdlSemanticsReferentialConstraint, IEdmAssociationEnd>();

		// Token: 0x04000480 RID: 1152
		private static readonly Func<CsdlSemanticsReferentialConstraint, IEdmAssociationEnd> ComputePrincipalFunc = (CsdlSemanticsReferentialConstraint me) => me.ComputePrincipal();

		// Token: 0x04000481 RID: 1153
		private readonly Cache<CsdlSemanticsReferentialConstraint, IEnumerable<IEdmStructuralProperty>> dependentPropertiesCache = new Cache<CsdlSemanticsReferentialConstraint, IEnumerable<IEdmStructuralProperty>>();

		// Token: 0x04000482 RID: 1154
		private static readonly Func<CsdlSemanticsReferentialConstraint, IEnumerable<IEdmStructuralProperty>> ComputeDependentPropertiesFunc = (CsdlSemanticsReferentialConstraint me) => me.ComputeDependentProperties();

		// Token: 0x04000483 RID: 1155
		private readonly Cache<CsdlSemanticsReferentialConstraint, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsReferentialConstraint, IEnumerable<EdmError>>();

		// Token: 0x04000484 RID: 1156
		private static readonly Func<CsdlSemanticsReferentialConstraint, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsReferentialConstraint me) => me.ComputeErrors();

		// Token: 0x04000485 RID: 1157
		private readonly Cache<CsdlSemanticsReferentialConstraint, IEnumerable<string>> principalKeyPropertiesNotFoundInPrincipalPropertiesCache = new Cache<CsdlSemanticsReferentialConstraint, IEnumerable<string>>();

		// Token: 0x04000486 RID: 1158
		private static readonly Func<CsdlSemanticsReferentialConstraint, IEnumerable<string>> ComputePrincipalKeyPropertiesNotFoundInPrincipalPropertiesFunc = (CsdlSemanticsReferentialConstraint me) => me.ComputePrincipalKeyPropertiesNotFoundInPrincipalProperties();

		// Token: 0x04000487 RID: 1159
		private readonly Cache<CsdlSemanticsReferentialConstraint, IEnumerable<string>> dependentPropertiesNotFoundInDependentTypeCache = new Cache<CsdlSemanticsReferentialConstraint, IEnumerable<string>>();

		// Token: 0x04000488 RID: 1160
		private static readonly Func<CsdlSemanticsReferentialConstraint, IEnumerable<string>> ComputeDependentPropertiesNotFoundInDependentTypeFunc = (CsdlSemanticsReferentialConstraint me) => me.ComputeDependentPropertiesNotFoundInDependentType();
	}
}
