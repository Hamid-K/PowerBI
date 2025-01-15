using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018E RID: 398
	internal abstract class CsdlSemanticsOperation : CsdlSemanticsElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001CC0C File Offset: 0x0001AE0C
		public CsdlSemanticsOperation(CsdlSemanticsSchema context, CsdlOperation operation)
			: base(operation)
		{
			this.Context = context;
			this.operation = operation;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000AA6 RID: 2726
		public abstract EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001CC44 File Offset: 0x0001AE44
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001CC51 File Offset: 0x0001AE51
		public string Name
		{
			get
			{
				return this.operation.Name;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0001CC5E File Offset: 0x0001AE5E
		public override CsdlElement Element
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001CC66 File Offset: 0x0001AE66
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0001CC73 File Offset: 0x0001AE73
		public bool IsBound
		{
			get
			{
				return this.operation.IsBound;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001CC80 File Offset: 0x0001AE80
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return this.entitySetPathCache.GetValue(this, CsdlSemanticsOperation.ComputeEntitySetPathFunc, null);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001CC94 File Offset: 0x0001AE94
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnTypeCache.GetValue(this, CsdlSemanticsOperation.ComputeReturnTypeFunc, null);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.parametersCache.GetValue(this, CsdlSemanticsOperation.ComputeParametersFunc, null);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		public CsdlSemanticsSchema Context { get; private set; }

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		public IEdmOperationParameter FindParameter(string name)
		{
			return Enumerable.SingleOrDefault<IEdmOperationParameter>(this.Parameters, (IEdmOperationParameter p) => p.Name == name);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001CD01 File Offset: 0x0001AF01
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001CD15 File Offset: 0x0001AF15
		private IEdmPathExpression ComputeEntitySetPath()
		{
			if (this.operation.EntitySetPath != null)
			{
				return new CsdlSemanticsOperation.OperationPathExpression(this.operation.EntitySetPath)
				{
					Location = base.Location
				};
			}
			return null;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001CD42 File Offset: 0x0001AF42
		private IEdmTypeReference ComputeReturnType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.Context, this.operation.ReturnType);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001CD5C File Offset: 0x0001AF5C
		private IEnumerable<IEdmOperationParameter> ComputeParameters()
		{
			List<IEdmOperationParameter> list = new List<IEdmOperationParameter>();
			foreach (CsdlOperationParameter csdlOperationParameter in this.operation.Parameters)
			{
				if (csdlOperationParameter.IsOptional)
				{
					list.Add(new CsdlSemanticsOptionalParameter(this, csdlOperationParameter, csdlOperationParameter.DefaultValue));
				}
				else
				{
					list.Add(new CsdlSemanticsOperationParameter(this, csdlOperationParameter));
				}
			}
			return list;
		}

		// Token: 0x0400063D RID: 1597
		private readonly CsdlOperation operation;

		// Token: 0x0400063E RID: 1598
		private readonly Cache<CsdlSemanticsOperation, IEdmPathExpression> entitySetPathCache = new Cache<CsdlSemanticsOperation, IEdmPathExpression>();

		// Token: 0x0400063F RID: 1599
		private static readonly Func<CsdlSemanticsOperation, IEdmPathExpression> ComputeEntitySetPathFunc = (CsdlSemanticsOperation me) => me.ComputeEntitySetPath();

		// Token: 0x04000640 RID: 1600
		private readonly Cache<CsdlSemanticsOperation, IEdmTypeReference> returnTypeCache = new Cache<CsdlSemanticsOperation, IEdmTypeReference>();

		// Token: 0x04000641 RID: 1601
		private static readonly Func<CsdlSemanticsOperation, IEdmTypeReference> ComputeReturnTypeFunc = (CsdlSemanticsOperation me) => me.ComputeReturnType();

		// Token: 0x04000642 RID: 1602
		private readonly Cache<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>> parametersCache = new Cache<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>>();

		// Token: 0x04000643 RID: 1603
		private static readonly Func<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>> ComputeParametersFunc = (CsdlSemanticsOperation me) => me.ComputeParameters();

		// Token: 0x020002C8 RID: 712
		private sealed class OperationPathExpression : EdmPathExpression, IEdmLocatable
		{
			// Token: 0x06001035 RID: 4149 RVA: 0x00013B8B File Offset: 0x00011D8B
			internal OperationPathExpression(string path)
				: base(path)
			{
			}

			// Token: 0x170004B0 RID: 1200
			// (get) Token: 0x06001036 RID: 4150 RVA: 0x0002B2C1 File Offset: 0x000294C1
			// (set) Token: 0x06001037 RID: 4151 RVA: 0x0002B2C9 File Offset: 0x000294C9
			public EdmLocation Location { get; set; }
		}
	}
}
