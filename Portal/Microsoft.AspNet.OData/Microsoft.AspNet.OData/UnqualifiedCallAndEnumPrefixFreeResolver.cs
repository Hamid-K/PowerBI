using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000057 RID: 87
	public class UnqualifiedCallAndEnumPrefixFreeResolver : ODataUriResolver
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A95C File Offset: 0x00008B5C
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000A964 File Offset: 0x00008B64
		public override bool EnableCaseInsensitive
		{
			get
			{
				return this._enableCaseInsensitive;
			}
			set
			{
				this._enableCaseInsensitive = value;
				this._stringAsEnum.EnableCaseInsensitive = this._enableCaseInsensitive;
				this._unqualified.EnableCaseInsensitive = this._enableCaseInsensitive;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A98F File Offset: 0x00008B8F
		public override IEnumerable<IEdmOperation> ResolveUnboundOperations(IEdmModel model, string identifier)
		{
			return this._unqualified.ResolveUnboundOperations(model, identifier);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A99E File Offset: 0x00008B9E
		public override IEnumerable<IEdmOperation> ResolveBoundOperations(IEdmModel model, string identifier, IEdmType bindingType)
		{
			return this._unqualified.ResolveBoundOperations(model, identifier, bindingType);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A9AE File Offset: 0x00008BAE
		public override void PromoteBinaryOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode leftNode, ref SingleValueNode rightNode, out IEdmTypeReference typeReference)
		{
			this._stringAsEnum.PromoteBinaryOperandTypes(binaryOperatorKind, ref leftNode, ref rightNode, out typeReference);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			return this._stringAsEnum.ResolveKeys(type, namedValues, convertFunc);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IList<string> positionalValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			return this._stringAsEnum.ResolveKeys(type, positionalValues, convertFunc);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public override IDictionary<IEdmOperationParameter, SingleValueNode> ResolveOperationParameters(IEdmOperation operation, IDictionary<string, SingleValueNode> input)
		{
			return this._stringAsEnum.ResolveOperationParameters(operation, input);
		}

		// Token: 0x040000BA RID: 186
		private readonly StringAsEnumResolver _stringAsEnum = new StringAsEnumResolver();

		// Token: 0x040000BB RID: 187
		private readonly UnqualifiedODataUriResolver _unqualified = new UnqualifiedODataUriResolver();

		// Token: 0x040000BC RID: 188
		private bool _enableCaseInsensitive;
	}
}
