using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Client.ALinq.UriParser;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x0200001E RID: 30
	internal class SelectExpandPathBuilder
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public SelectExpandPathBuilder()
		{
			this.firstSegmentInNewPath = true;
			this.uriVersion = Util.ODataVersion4;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004E1E File Offset: 0x0000301E
		public IEnumerable<string> ProjectionPaths
		{
			get
			{
				return this.WriteProjectionPaths();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004E26 File Offset: 0x00003026
		public IEnumerable<string> ExpandPaths
		{
			get
			{
				return this.WriteExpansionPaths();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004E2E File Offset: 0x0000302E
		public Version UriVersion
		{
			get
			{
				return this.uriVersion;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004E36 File Offset: 0x00003036
		public ParameterExpression ParamExpressionInScope
		{
			get
			{
				return this.parameterExpressions.Peek();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004E44 File Offset: 0x00003044
		public void PushParamExpression(ParameterExpression pe)
		{
			PathSegmentToken pathSegmentToken = this.expandPaths.LastOrDefault<PathSegmentToken>();
			this.basePaths.Add(pe, pathSegmentToken);
			this.expandPaths.Remove(pathSegmentToken);
			this.parameterExpressions.Push(pe);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004E83 File Offset: 0x00003083
		public void PopParamExpression()
		{
			this.parameterExpressions.Pop();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004E94 File Offset: 0x00003094
		public void StartNewPath()
		{
			PathSegmentToken pathSegmentToken = this.basePaths[this.ParamExpressionInScope];
			PathSegmentToken pathSegmentToken2;
			if (pathSegmentToken != null)
			{
				NewTreeBuilder newTreeBuilder = new NewTreeBuilder();
				pathSegmentToken2 = pathSegmentToken.Accept<PathSegmentToken>(newTreeBuilder);
			}
			else
			{
				pathSegmentToken2 = null;
			}
			this.expandPaths.Add(pathSegmentToken2);
			this.firstSegmentInNewPath = true;
			this.basePathIsEmpty = pathSegmentToken == null;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004EE8 File Offset: 0x000030E8
		public void AppendPropertyToPath(PropertyInfo pi, Type convertedSourceType, DataServiceContext context)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(pi.PropertyType);
			string text = ((convertedSourceType == null) ? null : UriHelper.GetEntityTypeNameForUriAndValidateMaxProtocolVersion(convertedSourceType, context, ref this.uriVersion));
			string serverDefinedName = ClientTypeUtil.GetServerDefinedName(pi);
			string text2 = ((convertedSourceType != null) ? string.Join('/'.ToString(), new string[] { text, serverDefinedName }) : serverDefinedName);
			if (flag)
			{
				this.AppendToExpandPath(text2, false);
			}
			else if (this.firstSegmentInNewPath && this.basePathIsEmpty)
			{
				this.AppendToProjectionPath(text2);
			}
			else
			{
				this.AppendToExpandPath(text2, true);
			}
			this.firstSegmentInNewPath = false;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004F84 File Offset: 0x00003184
		private IEnumerable<string> WriteExpansionPaths()
		{
			SelectExpandPathToStringVisitor visitor = new SelectExpandPathToStringVisitor();
			return from path in this.expandPaths
				where path != null
				select path.Accept<string>(visitor);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004FE0 File Offset: 0x000031E0
		private IEnumerable<string> WriteProjectionPaths()
		{
			return from path in this.projectionPaths
				where path != null
				select path.Identifier;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000503C File Offset: 0x0000323C
		private void AppendToProjectionPath(string name)
		{
			foreach (PathSegmentToken pathSegmentToken in this.projectionPaths)
			{
				if (pathSegmentToken != null && pathSegmentToken.Identifier == '*'.ToString())
				{
					this.projectionPaths.Remove(pathSegmentToken);
				}
			}
			this.projectionPaths.Add(new NonSystemToken(name, null, null));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000050C4 File Offset: 0x000032C4
		private void AppendToExpandPath(string name, bool isStructural)
		{
			PathSegmentToken pathSegmentToken = this.expandPaths.LastOrDefault<PathSegmentToken>();
			NonSystemToken nonSystemToken = new NonSystemToken(name, null, null);
			nonSystemToken.IsStructuralProperty = isStructural;
			if (pathSegmentToken != null)
			{
				this.expandPaths.Remove(pathSegmentToken);
				AddNewEndingTokenVisitor addNewEndingTokenVisitor = new AddNewEndingTokenVisitor(nonSystemToken);
				pathSegmentToken.Accept(addNewEndingTokenVisitor);
				this.expandPaths.Add(pathSegmentToken);
				return;
			}
			this.expandPaths.Add(nonSystemToken);
		}

		// Token: 0x04000044 RID: 68
		private const char EntireEntityMarker = '*';

		// Token: 0x04000045 RID: 69
		private readonly List<PathSegmentToken> projectionPaths = new List<PathSegmentToken>();

		// Token: 0x04000046 RID: 70
		private readonly List<PathSegmentToken> expandPaths = new List<PathSegmentToken>();

		// Token: 0x04000047 RID: 71
		private readonly Stack<ParameterExpression> parameterExpressions = new Stack<ParameterExpression>();

		// Token: 0x04000048 RID: 72
		private readonly Dictionary<ParameterExpression, PathSegmentToken> basePaths = new Dictionary<ParameterExpression, PathSegmentToken>(ReferenceEqualityComparer<ParameterExpression>.Instance);

		// Token: 0x04000049 RID: 73
		private Version uriVersion;

		// Token: 0x0400004A RID: 74
		private bool firstSegmentInNewPath;

		// Token: 0x0400004B RID: 75
		private bool basePathIsEmpty;
	}
}
