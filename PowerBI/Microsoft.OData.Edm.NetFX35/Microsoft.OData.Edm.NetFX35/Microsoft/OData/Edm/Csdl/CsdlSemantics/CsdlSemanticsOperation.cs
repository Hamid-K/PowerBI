using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200005E RID: 94
	internal abstract class CsdlSemanticsOperation : CsdlSemanticsElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00003DE6 File Offset: 0x00001FE6
		public CsdlSemanticsOperation(CsdlSemanticsSchema context, CsdlOperation operation)
			: base(operation)
		{
			this.Context = context;
			this.operation = operation;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600015E RID: 350
		public abstract EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003E1E File Offset: 0x0000201E
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003E2B File Offset: 0x0000202B
		public string Name
		{
			get
			{
				return this.operation.Name;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003E38 File Offset: 0x00002038
		public override CsdlElement Element
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003E40 File Offset: 0x00002040
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003E4D File Offset: 0x0000204D
		public bool IsBound
		{
			get
			{
				return this.operation.IsBound;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00003E5A File Offset: 0x0000205A
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return this.entitySetPathCache.GetValue(this, CsdlSemanticsOperation.ComputeEntitySetPathFunc, null);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003E6E File Offset: 0x0000206E
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnTypeCache.GetValue(this, CsdlSemanticsOperation.ComputeReturnTypeFunc, null);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00003E82 File Offset: 0x00002082
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.parametersCache.GetValue(this, CsdlSemanticsOperation.ComputeParametersFunc, null);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003E96 File Offset: 0x00002096
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00003E9E File Offset: 0x0000209E
		public CsdlSemanticsSchema Context { get; private set; }

		// Token: 0x06000169 RID: 361 RVA: 0x00003EC4 File Offset: 0x000020C4
		public IEdmOperationParameter FindParameter(string name)
		{
			return Enumerable.SingleOrDefault<IEdmOperationParameter>(this.Parameters, (IEdmOperationParameter p) => p.Name == name);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003EF5 File Offset: 0x000020F5
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003F0C File Offset: 0x0000210C
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

		// Token: 0x0600016C RID: 364 RVA: 0x00003F46 File Offset: 0x00002146
		private IEdmTypeReference ComputeReturnType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.Context, this.operation.ReturnType);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003F60 File Offset: 0x00002160
		private IEnumerable<IEdmOperationParameter> ComputeParameters()
		{
			List<IEdmOperationParameter> list = new List<IEdmOperationParameter>();
			foreach (CsdlOperationParameter csdlOperationParameter in this.operation.Parameters)
			{
				list.Add(new CsdlSemanticsOperationParameter(this, csdlOperationParameter));
			}
			return list;
		}

		// Token: 0x04000073 RID: 115
		private readonly CsdlOperation operation;

		// Token: 0x04000074 RID: 116
		private readonly Cache<CsdlSemanticsOperation, IEdmPathExpression> entitySetPathCache = new Cache<CsdlSemanticsOperation, IEdmPathExpression>();

		// Token: 0x04000075 RID: 117
		private static readonly Func<CsdlSemanticsOperation, IEdmPathExpression> ComputeEntitySetPathFunc = (CsdlSemanticsOperation me) => me.ComputeEntitySetPath();

		// Token: 0x04000076 RID: 118
		private readonly Cache<CsdlSemanticsOperation, IEdmTypeReference> returnTypeCache = new Cache<CsdlSemanticsOperation, IEdmTypeReference>();

		// Token: 0x04000077 RID: 119
		private static readonly Func<CsdlSemanticsOperation, IEdmTypeReference> ComputeReturnTypeFunc = (CsdlSemanticsOperation me) => me.ComputeReturnType();

		// Token: 0x04000078 RID: 120
		private readonly Cache<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>> parametersCache = new Cache<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>>();

		// Token: 0x04000079 RID: 121
		private static readonly Func<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>> ComputeParametersFunc = (CsdlSemanticsOperation me) => me.ComputeParameters();

		// Token: 0x02000060 RID: 96
		private sealed class OperationPathExpression : EdmPathExpression, IEdmLocatable
		{
			// Token: 0x06000177 RID: 375 RVA: 0x0000410C File Offset: 0x0000230C
			internal OperationPathExpression(string path)
				: base(path)
			{
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000178 RID: 376 RVA: 0x00004115 File Offset: 0x00002315
			// (set) Token: 0x06000179 RID: 377 RVA: 0x0000411D File Offset: 0x0000231D
			public EdmLocation Location { get; set; }
		}
	}
}
