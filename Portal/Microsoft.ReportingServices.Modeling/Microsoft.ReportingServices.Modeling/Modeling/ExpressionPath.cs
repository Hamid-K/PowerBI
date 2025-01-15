using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A9 RID: 169
	public sealed class ExpressionPath : CheckedCollection<PathItem>, ICloneable, IXmlLoadable, IPersistable
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0001DF62 File Offset: 0x0001C162
		public ExpressionPath()
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001DF6A File Offset: 0x0001C16A
		public ExpressionPath(params PathItem[] pathItems)
		{
			base.AddRange(pathItems);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001DF79 File Offset: 0x0001C179
		public ExpressionPath(ExpressionPath original, IEnumerable<PathItem> additionalItems)
		{
			base.AddRange(original);
			base.AddRange(additionalItems);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001DF8F File Offset: 0x0001C18F
		public ExpressionPath(ExpressionPath original, params PathItem[] additionalItems)
			: this(original, additionalItems)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001DF99 File Offset: 0x0001C199
		internal ExpressionPath(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001DFA2 File Offset: 0x0001C1A2
		public int Length
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001DFAA File Offset: 0x0001C1AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("The Length property must be used instead.", true)]
		public new int Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001DFB2 File Offset: 0x0001C1B2
		public PathItem LastItem
		{
			get
			{
				if (this.Length <= 0)
				{
					return null;
				}
				return base[this.Length - 1];
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001DFCD File Offset: 0x0001C1CD
		public override int GetHashCode()
		{
			return PathAlgorithms.GetHashCode<PathItem>(this);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		public override string ToString()
		{
			string[] array = new string[this.Length];
			for (int i = 0; i < this.Length; i++)
			{
				array[i] = base[i].Name;
			}
			return string.Join("/", array);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001E01C File Offset: 0x0001C21C
		public ExpressionPath Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001E025 File Offset: 0x0001C225
		public ExpressionPath Clone(ExpressionCopyManager copyManager)
		{
			ExpressionPath expressionPath = new ExpressionPath(this.Length);
			expressionPath.AddRange(this);
			return expressionPath;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001E039 File Offset: 0x0001C239
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001E041 File Offset: 0x0001C241
		public bool IsSameAs(ExpressionPath other)
		{
			return other != null && CollectionUtil.ElementsEqual<PathItem>(this, other);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001E050 File Offset: 0x0001C250
		public bool HasInvalidRefs()
		{
			using (List<PathItem>.Enumerator enumerator = base.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasInvalidRefs())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		public CardinalityContext GetCardinalityContext()
		{
			return PathAlgorithms.GetCardinalityContext<PathItem>(this);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		public CardinalityContext GetCardinalityContext(int atPosition)
		{
			return PathAlgorithms.GetCardinalityContext<PathItem>(this, atPosition);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001E0B5 File Offset: 0x0001C2B5
		public Cardinality GetCardinality()
		{
			return PathAlgorithms.GetCardinality<PathItem>(this);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001E0BD File Offset: 0x0001C2BD
		public Optionality GetOptionality()
		{
			return PathAlgorithms.GetOptionality<PathItem>(this);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001E0C5 File Offset: 0x0001C2C5
		public int GetLeadingScalarLength()
		{
			return PathAlgorithms.GetLeadingScalarLength<PathItem>(this);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001E0CD File Offset: 0x0001C2CD
		public ExpressionPath GetSegment(int startAt)
		{
			return this.GetSegment(startAt, this.Length - startAt);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
		public ExpressionPath GetSegment(int startAt, int length)
		{
			if (startAt < 0 || startAt > this.Length)
			{
				throw new ArgumentOutOfRangeException("startAt");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (startAt + length > this.Length)
			{
				throw new ArgumentException();
			}
			ExpressionPath expressionPath = new ExpressionPath(length);
			for (int i = 0; i < length; i++)
			{
				expressionPath.Add(base[startAt + i]);
			}
			return expressionPath;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001E148 File Offset: 0x0001C348
		public int GetMatchingSegmentLength(int startAt, ExpressionPath other)
		{
			return PathAlgorithms.GetMatchingSegmentLength<PathItem>(startAt, this, other);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001E152 File Offset: 0x0001C352
		public bool StartsWith(ExpressionPath path)
		{
			return PathAlgorithms.StartsWith<PathItem>(this, path);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001E15B File Offset: 0x0001C35B
		public bool EndsWith(ExpressionPath path)
		{
			return PathAlgorithms.EndsWith<PathItem>(this, path);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001E164 File Offset: 0x0001C364
		public void Split(int atPosition, out ExpressionPath firstPart, out ExpressionPath lastPart)
		{
			if (atPosition < 0 || atPosition > this.Length)
			{
				throw new ArgumentOutOfRangeException("atPosition");
			}
			firstPart = new ExpressionPath(atPosition);
			lastPart = new ExpressionPath(this.Length - atPosition);
			for (int i = 0; i < this.Length; i++)
			{
				if (i < atPosition)
				{
					firstPart.Add(base[i]);
				}
				else
				{
					lastPart.Add(base[i]);
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001E1D3 File Offset: 0x0001C3D3
		public void ReplaceWith(IEnumerable<PathItem> items)
		{
			base.Clear();
			if (items != null)
			{
				base.AddRange(items);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
		public void Reverse()
		{
			for (int i = 0; i < this.Length; i++)
			{
				base[i] = base[i].CreateReverse();
			}
			base.Items.Reverse();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001E224 File Offset: 0x0001C424
		public void TrimToMatchingSegment(ExpressionPath other)
		{
			int matchingSegmentLength = this.GetMatchingSegmentLength(0, other);
			if (matchingSegmentLength < this.Length)
			{
				base.RemoveRange(matchingSegmentLength, this.Length - matchingSegmentLength);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001E252 File Offset: 0x0001C452
		internal IQueryEntity SourceEntity
		{
			get
			{
				if (this.Length <= 0)
				{
					return null;
				}
				return base[0].SourceEntity;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001E26B File Offset: 0x0001C46B
		internal void AddRangeInternal(ExpressionPath path)
		{
			base.Items.AddRange(path);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001E279 File Offset: 0x0001C479
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.LoadObject(this);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001E288 File Offset: 0x0001C488
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001E28C File Offset: 0x0001C48C
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			PathItem pathItem = null;
			if (xr.IsDefaultNamespace && xr.LocalName == "RolePathItem")
			{
				pathItem = new RolePathItem();
			}
			if (pathItem != null)
			{
				pathItem.Load(xr);
				base.Add(pathItem);
				return true;
			}
			return false;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001E2CF File Offset: 0x0001C4CF
		internal void WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw, "Path");
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001E2E0 File Offset: 0x0001C4E0
		internal void WriteTo(ModelingXmlWriter xw, string pathElementName)
		{
			Predicate<PathItem> predicate = (PathItem item) => item.ShouldSerialize;
			if (base.Items.Exists(predicate))
			{
				xw.WriteStartElement(pathElementName);
				foreach (PathItem pathItem in Iterators.Filter<PathItem>(this, predicate))
				{
					pathItem.WriteTo(xw);
				}
				xw.WriteEndElement();
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001E368 File Offset: 0x0001C568
		internal ExpressionPath Compile(CompilationContext ctx, IQueryEntity desiredTargetEntity, out IQueryEntity actualTargetEntity)
		{
			ExpressionPath expressionPath = new ExpressionPath(this.Length);
			IQueryEntity queryEntity = ctx.ContextEntity;
			foreach (PathItem pathItem in this)
			{
				if (!pathItem.CheckInvalidRefs(ctx, false))
				{
					actualTargetEntity = null;
					return null;
				}
				if (pathItem.TargetEntity == null)
				{
					if (!pathItem.CheckInvalidRefs(ctx, true))
					{
						actualTargetEntity = null;
						return null;
					}
					throw new InternalModelingException("Can not process path item with null target entity.");
				}
				else
				{
					if (queryEntity != null)
					{
						if (pathItem.SourceEntity != null && pathItem.SourceEntity.Model != queryEntity.Model)
						{
							ctx.AddScopedError(ModelingErrorCode.WrongSemanticModel, SRErrors.WrongSemanticModel_QueryItemMultipleProperties(pathItem.PropertyName, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(pathItem.SourceEntity)));
							actualTargetEntity = null;
							return null;
						}
						if (pathItem.TargetEntity.Model != queryEntity.Model)
						{
							ctx.AddScopedError(ModelingErrorCode.WrongSemanticModel, SRErrors.WrongSemanticModel_QueryItemMultipleProperties(pathItem.PropertyName, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(pathItem.TargetEntity)));
							actualTargetEntity = null;
							return null;
						}
					}
					if (!ExpressionPath.ResolvePath(queryEntity, pathItem.SourceEntity, expressionPath))
					{
						ctx.PushContextEntity(queryEntity);
						try
						{
							pathItem.AddOutOfContextError(ctx);
						}
						finally
						{
							ctx.PopContextEntity();
						}
						actualTargetEntity = null;
						return null;
					}
					expressionPath.Add(pathItem);
					queryEntity = pathItem.TargetEntity;
				}
			}
			if (desiredTargetEntity != null && ExpressionPath.ResolvePath(queryEntity, desiredTargetEntity, expressionPath))
			{
				queryEntity = desiredTargetEntity;
			}
			actualTargetEntity = queryEntity;
			if (ctx.ShouldPersist)
			{
				if (ctx.ShouldNormalize)
				{
					this.ReplaceWith(expressionPath);
				}
				base.SetReadOnlyIndicator();
				return expressionPath;
			}
			return expressionPath;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001E530 File Offset: 0x0001C730
		private static bool ResolvePath(IQueryEntity sourceEntity, IQueryEntity targetEntity, ExpressionPath resolvedPath)
		{
			return sourceEntity == null || targetEntity == null || targetEntity == sourceEntity || ExpressionPath.ResolveInheritancePath(sourceEntity, targetEntity, resolvedPath);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001E550 File Offset: 0x0001C750
		private static bool ResolveInheritancePath(IQueryEntity sourceEntity, IQueryEntity targetEntity, ExpressionPath resolvedPath)
		{
			int num;
			int num2;
			if (ExpressionPath.GetInheritanceRoot(sourceEntity, out num) != ExpressionPath.GetInheritanceRoot(targetEntity, out num2))
			{
				return false;
			}
			ExpressionPath expressionPath = new ExpressionPath(num);
			ExpressionPath expressionPath2 = new ExpressionPath(num2);
			if (num > num2)
			{
				ExpressionPath.AddInheritancePathItems(expressionPath, ref sourceEntity, ref num, num2);
			}
			else if (num2 > num)
			{
				ExpressionPath.AddInheritancePathItems(expressionPath2, ref targetEntity, ref num2, num);
			}
			if (num != num2)
			{
				throw new InternalModelingException("sourceDepth does not equal targetDepth after evening-out");
			}
			while (sourceEntity != targetEntity)
			{
				if (sourceEntity.InheritsFrom == targetEntity.InheritsFrom && sourceEntity.InheritsFrom.DisjointInheritance)
				{
					return false;
				}
				expressionPath.Add(new InheritancePathItem(sourceEntity, sourceEntity.InheritsFrom));
				expressionPath2.Add(new InheritancePathItem(targetEntity, targetEntity.InheritsFrom));
				sourceEntity = sourceEntity.InheritsFrom;
				targetEntity = targetEntity.InheritsFrom;
				num--;
				if (num < 0)
				{
					throw new InternalModelingException("sourceDepth passed zero without reaching common base entity");
				}
			}
			resolvedPath.AddRange(expressionPath);
			expressionPath2.Reverse();
			resolvedPath.AddRange(expressionPath2);
			return true;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001E62C File Offset: 0x0001C82C
		private static IQueryEntity GetInheritanceRoot(IQueryEntity entity, out int depth)
		{
			IQueryEntity queryEntity = entity;
			depth = 0;
			while (queryEntity.InheritsFrom != null)
			{
				queryEntity = queryEntity.InheritsFrom;
				depth++;
			}
			return queryEntity;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001E656 File Offset: 0x0001C856
		private static void AddInheritancePathItems(ExpressionPath path, ref IQueryEntity entity, ref int entityDepth, int stopDepth)
		{
			if (entityDepth < stopDepth)
			{
				throw new InternalModelingException("entityDepth is less than stopDepth");
			}
			while (entityDepth > stopDepth)
			{
				path.Add(new InheritancePathItem(entity, entity.InheritsFrom));
				entity = entity.InheritsFrom;
				entityDepth--;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001E690 File Offset: 0x0001C890
		internal Expression TryGetFirstSecurityFilterCondition(CompilationContext ctx, out ExpressionPath pathBeforeSecurityFilter, out ExpressionPath pathAfterSecurityFilter)
		{
			pathBeforeSecurityFilter = null;
			pathAfterSecurityFilter = null;
			for (int i = 0; i < this.Length; i++)
			{
				Expression expression = base[i].TargetEntityInternal.TryGetSecurityFilterCondition(ctx);
				if (expression != null)
				{
					pathBeforeSecurityFilter = this.GetSegment(0, i + 1);
					pathAfterSecurityFilter = this.GetSegment(i + 1);
					return expression;
				}
			}
			return null;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		internal ExpressionPath CloneFor(SemanticModel newModel)
		{
			ExpressionPath expressionPath = new ExpressionPath(this.Length);
			if (!CheckedCollection<PathItem>.LazyCloneItems<PathItem>(this, expressionPath, true, newModel))
			{
				return null;
			}
			return expressionPath;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001E70B File Offset: 0x0001C90B
		private static ExpressionPath CreateEmptyInstance()
		{
			ExpressionPath expressionPath = new ExpressionPath();
			expressionPath.SetReadOnlyIndicator();
			return expressionPath;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001E718 File Offset: 0x0001C918
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001E724 File Offset: 0x0001C924
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ExpressionPath.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Items)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.WriteRIFList<PathItem>(this);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001E793 File Offset: 0x0001C993
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001E79C File Offset: 0x0001C99C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (this.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ExpressionPath.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					reader.ReadListOfRIFObjects(this);
				}
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001E830 File Offset: 0x0001CA30
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001E83C File Offset: 0x0001CA3C
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.ExpressionPath;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001E840 File Offset: 0x0001CA40
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ExpressionPath.__declaration, ExpressionPath.__declarationLock, () => new Declaration(ObjectType.ExpressionPath, ObjectType.CheckedCollection, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, ObjectType.PathItem)
				}));
			}
		}

		// Token: 0x040003DE RID: 990
		internal const string PathElem = "Path";

		// Token: 0x040003DF RID: 991
		public static readonly ExpressionPath Empty = ExpressionPath.CreateEmptyInstance();

		// Token: 0x040003E0 RID: 992
		private static Declaration __declaration;

		// Token: 0x040003E1 RID: 993
		private static readonly object __declarationLock = new object();
	}
}
