using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004C RID: 76
	internal class AmbiguousOperationBinding : AmbiguousBinding<IEdmOperation>, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00004B61 File Offset: 0x00002D61
		public AmbiguousOperationBinding(IEdmOperation first, IEdmOperation second)
			: base(first, second)
		{
			this.first = first;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.Namespace, base.Name);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004B89 File Offset: 0x00002D89
		public string Namespace
		{
			get
			{
				return this.first.Namespace;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00004B96 File Offset: 0x00002D96
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00004B9E File Offset: 0x00002D9E
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.first.Parameters;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00004BAB File Offset: 0x00002DAB
		public bool IsBound
		{
			get
			{
				return this.first.IsBound;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return this.first.EntitySetPath;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004BC5 File Offset: 0x00002DC5
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return this.first.SchemaElementKind;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004BD2 File Offset: 0x00002DD2
		public IEdmOperationParameter FindParameter(string name)
		{
			return this.first.FindParameter(name);
		}

		// Token: 0x04000090 RID: 144
		private readonly string fullName;

		// Token: 0x04000091 RID: 145
		private IEdmOperation first;
	}
}
