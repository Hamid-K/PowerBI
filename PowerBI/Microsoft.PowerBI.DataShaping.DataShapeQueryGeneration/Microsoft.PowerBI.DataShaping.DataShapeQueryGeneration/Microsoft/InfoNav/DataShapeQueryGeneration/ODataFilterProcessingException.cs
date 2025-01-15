using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000091 RID: 145
	internal sealed class ODataFilterProcessingException : Exception
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x0001481E File Offset: 0x00012A1E
		internal ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode errorCode, params object[] args)
		{
			this._errorCode = errorCode;
			this._args = args;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00014834 File Offset: 0x00012A34
		internal string MessageTemplate
		{
			get
			{
				string text;
				if (!ODataFilterProcessingException.ErrorMessageTemplates.TryGetValue(this._errorCode, out text))
				{
					return this._errorCode.ToString();
				}
				return text;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00014868 File Offset: 0x00012A68
		internal object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00014870 File Offset: 0x00012A70
		public override string Message
		{
			get
			{
				return StringUtil.FormatInvariant(this.MessageTemplate, this._args);
			}
		}

		// Token: 0x0400031E RID: 798
		private static readonly Dictionary<ODataQueryFilterProcessingErrorCode, string> ErrorMessageTemplates = new Dictionary<ODataQueryFilterProcessingErrorCode, string>
		{
			{
				ODataQueryFilterProcessingErrorCode.BinaryNodeLeftMustBeConvert,
				"Left side must be a convert node."
			},
			{
				ODataQueryFilterProcessingErrorCode.BinaryNodeOperatorMustBeEquality,
				"Only equality binary operator is supported."
			},
			{
				ODataQueryFilterProcessingErrorCode.BinaryNodeRightMustBeLiteral,
				"Right side must be a literal."
			},
			{
				ODataQueryFilterProcessingErrorCode.ConvertNodeSourceMustBeProperty,
				"Convert source has to be an open property access node."
			},
			{
				ODataQueryFilterProcessingErrorCode.ConvertNodeTypeMustBeString,
				"Only conversion to string is supported."
			},
			{
				ODataQueryFilterProcessingErrorCode.EntityCouldNotBeResolved,
				"Referenced conceptual entity '{0}' could not be resolved."
			},
			{
				ODataQueryFilterProcessingErrorCode.EntityIsPresentMoreThanOnceInQuerySources,
				"Referenced conceptual entity '{0}' is represented more than once in the list of sources."
			},
			{
				ODataQueryFilterProcessingErrorCode.FilterClauseRootMustBeBinaryNode,
				"Only category filter expressions are supported."
			},
			{
				ODataQueryFilterProcessingErrorCode.LiteralNodeTypeMustBeString,
				"Only string literals are supported."
			},
			{
				ODataQueryFilterProcessingErrorCode.PropertyCouldNotBeResolved,
				"Referenced conceptual property '{0}' could not be resolved."
			},
			{
				ODataQueryFilterProcessingErrorCode.PropertyNodeMustHaveParentSource,
				"Property node needs to have a parent source."
			},
			{
				ODataQueryFilterProcessingErrorCode.PropertyNodeRootMustBeIterator,
				"Root node is expected to be '$it'."
			},
			{
				ODataQueryFilterProcessingErrorCode.PropertyNodeRootMustBeRangeVariableReference,
				"Root node needs to be a range variable reference."
			},
			{
				ODataQueryFilterProcessingErrorCode.PropertyNodeSchemaMustHaveParentSource,
				"Schema node needs to have a parent root."
			},
			{
				ODataQueryFilterProcessingErrorCode.PropertyNodeSourceMustBeProperty,
				"Property source needs to be an open property access node."
			},
			{
				ODataQueryFilterProcessingErrorCode.PropertyNodeSourceMustHaveParentSource,
				"EntityCollection node needs to have a parent."
			},
			{
				ODataQueryFilterProcessingErrorCode.QueryFilterCouldNotBeParsed,
				"The OData query filter failed to be parsed against an open model. Parser error: {0}."
			},
			{
				ODataQueryFilterProcessingErrorCode.UnsupportedNodeType,
				"Node of type '{0}' is not supported."
			}
		};

		// Token: 0x0400031F RID: 799
		private readonly ODataQueryFilterProcessingErrorCode _errorCode;

		// Token: 0x04000320 RID: 800
		private readonly object[] _args;
	}
}
