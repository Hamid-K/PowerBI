using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CE5 RID: 7397
	internal sealed class MemberLetPartitionedDocument : IMemberLetPartitionedDocument, IPartitionedDocument
	{
		// Token: 0x0600B8A4 RID: 47268 RVA: 0x002564F0 File Offset: 0x002546F0
		public MemberLetPartitionedDocument(IEngine engine, IPackage package)
		{
			this.syncRoot = new object();
			this.engine = engine;
			this.package = package;
		}

		// Token: 0x17002DB9 RID: 11705
		// (get) Token: 0x0600B8A5 RID: 47269 RVA: 0x00256511 File Offset: 0x00254711
		public IPackage Package
		{
			get
			{
				return this.package;
			}
		}

		// Token: 0x17002DBA RID: 11706
		// (get) Token: 0x0600B8A6 RID: 47270 RVA: 0x00002139 File Offset: 0x00000339
		public PartitioningScheme PartitioningScheme
		{
			get
			{
				return PartitioningScheme.MemberLet1;
			}
		}

		// Token: 0x17002DBB RID: 11707
		// (get) Token: 0x0600B8A7 RID: 47271 RVA: 0x00256519 File Offset: 0x00254719
		public IEnumerable<IPartitionKey> PartitionKeys
		{
			get
			{
				IPartitionKey[][] partitionKeys = this.GetPartitionKeys();
				int num;
				for (int i = 0; i < partitionKeys.Length; i = num + 1)
				{
					IPartitionKey[] sectionPartitionKeys = partitionKeys[i];
					for (int j = 0; j < sectionPartitionKeys.Length; j = num + 1)
					{
						yield return sectionPartitionKeys[j];
						num = j;
					}
					sectionPartitionKeys = null;
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x0600B8A8 RID: 47272 RVA: 0x0025652C File Offset: 0x0025472C
		public IEnumerable<IPartitionKey> GetPartitionInputs(IPartitionKey partitionKey)
		{
			IMemberLetPartitionKey memberLetPartitionKey = partitionKey as IMemberLetPartitionKey;
			if (memberLetPartitionKey != null)
			{
				Dictionary<IPartitionKey, HashSet<IPartitionKey>> dictionary = null;
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.sectionsPartitionInputs != null)
					{
						this.sectionsPartitionInputs.TryGetValue(memberLetPartitionKey.Section, out dictionary);
					}
				}
				ISectionDocument sectionDocument;
				if (dictionary == null && this.ParseSections().TryGetValue(memberLetPartitionKey.Section, out sectionDocument))
				{
					dictionary = new MemberLetPartitionedDocument.ReferenceVisitor(this).GetPartitionReferences(sectionDocument);
					obj = this.syncRoot;
					lock (obj)
					{
						if (this.sectionsPartitionInputs == null)
						{
							this.sectionsPartitionInputs = new Dictionary<string, Dictionary<IPartitionKey, HashSet<IPartitionKey>>>();
						}
						this.sectionsPartitionInputs[memberLetPartitionKey.Section] = dictionary;
					}
				}
				HashSet<IPartitionKey> hashSet;
				if (dictionary != null && dictionary.TryGetValue(partitionKey, out hashSet))
				{
					return hashSet;
				}
			}
			return EmptyArray<IPartitionKey>.Instance;
		}

		// Token: 0x0600B8A9 RID: 47273 RVA: 0x00002105 File Offset: 0x00000305
		public bool IsPartitionInError(IPartitionKey partitionKey)
		{
			return false;
		}

		// Token: 0x0600B8AA RID: 47274 RVA: 0x0025661C File Offset: 0x0025481C
		public bool IsPartitionResultOfMember(IPartitionKey partitionKey)
		{
			IMemberLetPartitionKey memberLetPartitionKey = (IMemberLetPartitionKey)partitionKey;
			return memberLetPartitionKey.Equals(this.GetSpecificPartitionKey(new MemberLetPartitionKey(memberLetPartitionKey.Section, memberLetPartitionKey.Member)));
		}

		// Token: 0x0600B8AB RID: 47275 RVA: 0x00256650 File Offset: 0x00254850
		public bool ArePartitionsOfSameMember(IPartitionKey partitionKey1, IPartitionKey partitionKey2)
		{
			IMemberLetPartitionKey memberLetPartitionKey = (IMemberLetPartitionKey)partitionKey1;
			IMemberLetPartitionKey memberLetPartitionKey2 = (IMemberLetPartitionKey)partitionKey2;
			return memberLetPartitionKey.Section == memberLetPartitionKey2.Section && memberLetPartitionKey.Member == memberLetPartitionKey2.Member;
		}

		// Token: 0x0600B8AC RID: 47276 RVA: 0x00256694 File Offset: 0x00254894
		public SegmentedString GetPartition(IPartitionKey partitionKey)
		{
			ISectionDocument sectionDocument;
			int num;
			int num2;
			if (this.TryGetPartitionSectionDocumentOffsetAndLength(partitionKey, out sectionDocument, out num, out num2))
			{
				return sectionDocument.Tokens.GetSegmentedText().Substring(num, num2);
			}
			return default(SegmentedString);
		}

		// Token: 0x0600B8AD RID: 47277 RVA: 0x002566CC File Offset: 0x002548CC
		public string GetPartitionSection(IPartitionKey partitionKey)
		{
			int num;
			int num2;
			return this.GetPartitionSectionOffsetAndLength(partitionKey, out num, out num2);
		}

		// Token: 0x0600B8AE RID: 47278 RVA: 0x002566E4 File Offset: 0x002548E4
		public string GetPartitionSectionOffsetAndLength(IPartitionKey partitionKey, out int offset, out int length)
		{
			string text;
			this.TryGetPartitionSectionOffsetAndLength(partitionKey, out text, out offset, out length);
			return text;
		}

		// Token: 0x0600B8AF RID: 47279 RVA: 0x00256700 File Offset: 0x00254900
		public ISectionDocument GetSectionDocument(string sectionName)
		{
			ISectionDocument sectionDocument;
			if (this.ParseSections().TryGetValue(sectionName, out sectionDocument))
			{
				return sectionDocument;
			}
			return null;
		}

		// Token: 0x0600B8B0 RID: 47280 RVA: 0x00256720 File Offset: 0x00254920
		private bool TryGetPartitionSectionOffsetAndLength(IPartitionKey partitionKey, out string sectionName, out int offset, out int length)
		{
			ISectionDocument sectionDocument;
			if (this.TryGetPartitionSectionDocumentOffsetAndLength(partitionKey, out sectionDocument, out offset, out length))
			{
				sectionName = sectionDocument.Section.SectionName;
				return true;
			}
			sectionName = null;
			return false;
		}

		// Token: 0x0600B8B1 RID: 47281 RVA: 0x00256754 File Offset: 0x00254954
		private bool TryGetPartitionSectionDocumentOffsetAndLength(IPartitionKey partitionKey, out ISectionDocument document, out int offset, out int length)
		{
			IMemberLetPartitionKey memberLetPartitionKey = (IMemberLetPartitionKey)partitionKey;
			ISectionMember sectionMember;
			if (this.TryGetSectionDocumentAndMember(memberLetPartitionKey, out document, out sectionMember))
			{
				TokenRange range = this.GetNode(sectionMember.Value, memberLetPartitionKey, -1).Range;
				int num = document.Tokens.GetOffset(range.End) + document.Tokens.GetLength(range.End);
				offset = document.Tokens.GetOffset(range.Start);
				length = num - offset;
				return true;
			}
			offset = -1;
			length = -1;
			return false;
		}

		// Token: 0x0600B8B2 RID: 47282 RVA: 0x002567D8 File Offset: 0x002549D8
		public bool TryGetOffsetAndLength(string sectionName, TextRange range, out int offset, out int length)
		{
			ISectionDocument sectionDocument;
			if (this.ParseSections().TryGetValue(sectionName, out sectionDocument))
			{
				offset = sectionDocument.Tokens.GetOffset(range.Start);
				length = sectionDocument.Tokens.GetOffset(range.End) - offset;
				return true;
			}
			offset = -1;
			length = -1;
			return false;
		}

		// Token: 0x0600B8B3 RID: 47283 RVA: 0x0025682C File Offset: 0x00254A2C
		public IPartitionKey GetPartitionKeyAndOffset(string sectionName, int offset, int length, out int partitionOffset)
		{
			partitionOffset = -1;
			ISectionDocument sectionDocument;
			if (this.ParseSections().TryGetValue(sectionName, out sectionDocument))
			{
				int num = offset + length;
				List<string> list = new List<string>();
				foreach (ISectionMember sectionMember in sectionDocument.Section.Members)
				{
					if (MemberLetPartitionedDocument.TryGetPath(sectionDocument, sectionMember.Value, offset, num, list, ref partitionOffset))
					{
						return new MemberLetPartitionKey(sectionName, sectionMember.Name, list.ToArray());
					}
				}
			}
			return null;
		}

		// Token: 0x0600B8B4 RID: 47284 RVA: 0x002568CC File Offset: 0x00254ACC
		private static bool TryGetPath(ISectionDocument document, IExpression expression, int start, int end, List<string> path, ref int partitionOffset)
		{
			if (expression.Range.IsNull)
			{
				return false;
			}
			int offset = document.Tokens.GetOffset(expression.Range.Start);
			int num = document.Tokens.GetOffset(expression.Range.End) + document.Tokens.GetLength(expression.Range.End);
			if (start < offset || end > num + 1)
			{
				return false;
			}
			partitionOffset = start - offset;
			ILetExpression letExpression = expression as ILetExpression;
			if (letExpression != null)
			{
				foreach (VariableInitializer variableInitializer in letExpression.Variables)
				{
					if (MemberLetPartitionedDocument.TryGetPath(document, variableInitializer.Value, start, end, path, ref partitionOffset))
					{
						path.Insert(0, variableInitializer.Name);
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x0600B8B5 RID: 47285 RVA: 0x002569C0 File Offset: 0x00254BC0
		public IEnumerable<PackageEdit> ReplacePartition(IPartitionKey partitionKey, SegmentedString expression)
		{
			string text;
			int num;
			int num2;
			if (this.TryGetPartitionSectionOffsetAndLength(partitionKey, out text, out num, out num2))
			{
				return new PackageEdit[]
				{
					new PackageEdit(text, num, num2, expression)
				};
			}
			return EmptyArray<PackageEdit>.Instance;
		}

		// Token: 0x0600B8B6 RID: 47286 RVA: 0x002569F8 File Offset: 0x00254BF8
		public IEnumerable<PackageEdit> ReferencePartition(IPartitionKey partitionKey, out string referencingExpression)
		{
			IMemberLetPartitionKey memberLetPartitionKey = (IMemberLetPartitionKey)partitionKey;
			List<PackageEdit> list = new List<PackageEdit>();
			ISectionDocument sectionDocument;
			ISectionMember sectionMember;
			if (this.TryGetSectionDocumentAndMember(memberLetPartitionKey, out sectionDocument, out sectionMember))
			{
				IExpression expression = sectionMember.Value;
				using (IEnumerator<string> enumerator = memberLetPartitionKey.Lets.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string name = enumerator.Current;
						ILetExpression letExpression = (ILetExpression)expression;
						VariableInitializer variableInitializer = letExpression.Variables.Where((VariableInitializer v) => v.Name == name).First<VariableInitializer>();
						IIdentifierExpression identifierExpression = letExpression.Expression as IIdentifierExpression;
						if (identifierExpression == null || identifierExpression.Name != name)
						{
							TokenRange range = letExpression.Expression.Range;
							int offset = sectionDocument.Tokens.GetOffset(range.Start);
							int num = sectionDocument.Tokens.GetOffset(range.End) + sectionDocument.Tokens.GetLength(range.End);
							list.Add(new PackageEdit(sectionDocument.Section.SectionName, offset, num - offset, SegmentedString.New(this.engine.EscapeIdentifier(variableInitializer.Name))));
						}
						expression = variableInitializer.Value;
					}
				}
			}
			referencingExpression = this.engine.EscapeIdentifier(memberLetPartitionKey.Section) + "!" + this.engine.EscapeIdentifier(memberLetPartitionKey.Member);
			return list;
		}

		// Token: 0x0600B8B7 RID: 47287 RVA: 0x00256BA0 File Offset: 0x00254DA0
		public IPartitionedDocument ApplyEdits(IEnumerable<PackageEdit> edits)
		{
			return new MemberLetPartitionedDocument(this.engine, this.package.ApplyEdits(edits));
		}

		// Token: 0x0600B8B8 RID: 47288 RVA: 0x00256BBC File Offset: 0x00254DBC
		private Dictionary<string, ISectionDocument> ParseSections()
		{
			if (this.sectionDocuments == null)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.sectionDocuments == null)
					{
						Dictionary<string, ISectionDocument> dictionary = new Dictionary<string, ISectionDocument>();
						foreach (string text in this.package.SectionNames)
						{
							IPackageSection section = this.package.GetSection(text);
							ITokens tokens = this.engine.Tokenize(section.Text);
							IDocument document = this.engine.Parse(tokens, section, delegate(IError error)
							{
							});
							dictionary.Add(text, (ISectionDocument)document);
						}
						this.sectionDocuments = dictionary;
					}
				}
			}
			return this.sectionDocuments;
		}

		// Token: 0x0600B8B9 RID: 47289 RVA: 0x00256CBC File Offset: 0x00254EBC
		private IPartitionKey[][] GetPartitionKeys()
		{
			if (this.partitionKeys == null)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.partitionKeys == null)
					{
						IPartitionKey[][] array = new IPartitionKey[this.ParseSections().Values.Count][];
						int num = 0;
						foreach (ISectionDocument sectionDocument in this.ParseSections().Values)
						{
							array[num] = this.GetPartitionKeys(sectionDocument);
							num++;
						}
						this.partitionKeys = array;
					}
				}
			}
			return this.partitionKeys;
		}

		// Token: 0x0600B8BA RID: 47290 RVA: 0x00256D80 File Offset: 0x00254F80
		private IPartitionKey[] GetPartitionKeys(ISectionDocument document)
		{
			MemberLetPartitionedDocument.DocumentKey documentKey;
			if (MemberLetPartitionedDocument.DocumentKey.TryCreate(document, out documentKey))
			{
				LruCache<MemberLetPartitionedDocument.DocumentKey, IPartitionKey[]> lruCache = MemberLetPartitionedDocument.partitionKeysCache;
				lock (lruCache)
				{
					IPartitionKey[] array;
					if (MemberLetPartitionedDocument.partitionKeysCache.TryGetValue(documentKey, out array))
					{
						return array;
					}
				}
			}
			IPartitionKey[] array2 = MemberLetPartitionedDocument.CreatePartitionKeys(new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance), document).ToArray<IPartitionKey>();
			if (documentKey != null)
			{
				LruCache<MemberLetPartitionedDocument.DocumentKey, IPartitionKey[]> lruCache = MemberLetPartitionedDocument.partitionKeysCache;
				lock (lruCache)
				{
					if (!MemberLetPartitionedDocument.partitionKeysCache.ContainsKey(documentKey))
					{
						MemberLetPartitionedDocument.partitionKeysCache.Add(documentKey, array2);
					}
				}
			}
			return array2;
		}

		// Token: 0x0600B8BB RID: 47291 RVA: 0x00256E38 File Offset: 0x00255038
		private bool TryGetSectionDocumentAndMember(IMemberLetPartitionKey partitionKey, out ISectionDocument document, out ISectionMember member)
		{
			member = null;
			if (this.ParseSections().TryGetValue(partitionKey.Section, out document))
			{
				member = document.Section.Members.Where((ISectionMember m) => m.Name == partitionKey.Member).FirstOrDefault<ISectionMember>();
			}
			return member != null;
		}

		// Token: 0x0600B8BC RID: 47292 RVA: 0x00256E98 File Offset: 0x00255098
		private IExpression GetNode(IExpression node, IMemberLetPartitionKey partitionKey, int nodeLetDepth = -1)
		{
			int num = 0;
			using (IEnumerator<string> enumerator = partitionKey.Lets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string name = enumerator.Current;
					if (num > nodeLetDepth)
					{
						node = ((ILetExpression)node).Variables.Where((VariableInitializer v) => v.Name == name).First<VariableInitializer>().Value;
					}
					num++;
				}
			}
			return node;
		}

		// Token: 0x0600B8BD RID: 47293 RVA: 0x00256F20 File Offset: 0x00255120
		public IMemberLetPartitionKey GetSpecificPartitionKey(IMemberLetPartitionKey partitionKey)
		{
			ISectionDocument sectionDocument;
			ISectionMember sectionMember;
			if (this.TryGetSectionDocumentAndMember(partitionKey, out sectionDocument, out sectionMember))
			{
				IExpression expression = this.GetNode(sectionMember.Value, partitionKey, -1);
				List<string> list = new List<string>(partitionKey.Lets);
				for (int i = list.Count; i < 1; i++)
				{
					ILetExpression letExpression = expression as ILetExpression;
					if (letExpression != null)
					{
						IIdentifierExpression identifierExpression = letExpression.Expression as IIdentifierExpression;
						if (identifierExpression != null)
						{
							for (int j = 0; j < letExpression.Variables.Count; j++)
							{
								VariableInitializer variableInitializer = letExpression.Variables[j];
								if (variableInitializer.Name == identifierExpression.Name)
								{
									list.Add(variableInitializer.Name);
									expression = variableInitializer.Value;
									break;
								}
							}
						}
					}
				}
				partitionKey = new MemberLetPartitionKey(partitionKey.Section, partitionKey.Member, list.ToArray());
			}
			return partitionKey;
		}

		// Token: 0x0600B8BE RID: 47294 RVA: 0x00257007 File Offset: 0x00255207
		private static IEnumerable<IPartitionKey> CreatePartitionKeys(HashSet<IPartitionKey> created, ISectionDocument document)
		{
			int num;
			for (int i = 0; i < document.Section.Members.Count; i = num + 1)
			{
				ISectionMember member = document.Section.Members[i];
				bool flag = true;
				ILetExpression letExpression = member.Value as ILetExpression;
				if (letExpression != null)
				{
					IEnumerable<IPartitionKey> enumerable = MemberLetPartitionedDocument.CreatePartitionKeys(created, document.Section.SectionName, member.Name, EmptyArray<string>.Instance, letExpression);
					foreach (IPartitionKey partitionKey in enumerable)
					{
						yield return partitionKey;
						flag = false;
					}
					IEnumerator<IPartitionKey> enumerator = null;
				}
				if (flag)
				{
					IPartitionKey partitionKey2 = new MemberLetPartitionKey(document.Section.SectionName, member.Name);
					if (created.Add(partitionKey2))
					{
						yield return partitionKey2;
					}
				}
				member = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600B8BF RID: 47295 RVA: 0x0025701E File Offset: 0x0025521E
		private static IEnumerable<IPartitionKey> CreatePartitionKeys(HashSet<IPartitionKey> created, string section, string member, string[] lets, ILetExpression let)
		{
			IIdentifierExpression identifierExpression = let.Expression as IIdentifierExpression;
			if (identifierExpression != null)
			{
				bool flag = false;
				for (int j = 0; j < let.Variables.Count; j++)
				{
					if (let.Variables[j].Name == identifierExpression.Name)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					int num;
					for (int i = 0; i < let.Variables.Count; i = num + 1)
					{
						VariableInitializer variableInitializer = let.Variables[i];
						string[] variableLets = lets.Add(variableInitializer.Name.Name);
						bool flag2 = true;
						ILetExpression letExpression = variableInitializer.Value as ILetExpression;
						if (lets.Length < 0 && letExpression != null)
						{
							IEnumerable<IPartitionKey> enumerable = MemberLetPartitionedDocument.CreatePartitionKeys(created, section, member, variableLets, letExpression);
							foreach (IPartitionKey partitionKey in enumerable)
							{
								yield return partitionKey;
								flag2 = false;
							}
							IEnumerator<IPartitionKey> enumerator = null;
						}
						if (flag2)
						{
							IPartitionKey partitionKey2 = new MemberLetPartitionKey(section, member, variableLets);
							if (created.Add(partitionKey2))
							{
								yield return partitionKey2;
							}
						}
						variableLets = null;
						num = i;
					}
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x04005DF1 RID: 24049
		private const int letLevels = 1;

		// Token: 0x04005DF2 RID: 24050
		private static readonly LruCache<MemberLetPartitionedDocument.DocumentKey, IPartitionKey[]> partitionKeysCache = new LruCache<MemberLetPartitionedDocument.DocumentKey, IPartitionKey[]>(16, null);

		// Token: 0x04005DF3 RID: 24051
		private readonly object syncRoot;

		// Token: 0x04005DF4 RID: 24052
		private readonly IEngine engine;

		// Token: 0x04005DF5 RID: 24053
		private readonly IPackage package;

		// Token: 0x04005DF6 RID: 24054
		private Dictionary<string, ISectionDocument> sectionDocuments;

		// Token: 0x04005DF7 RID: 24055
		private IPartitionKey[][] partitionKeys;

		// Token: 0x04005DF8 RID: 24056
		private Dictionary<string, Dictionary<IPartitionKey, HashSet<IPartitionKey>>> sectionsPartitionInputs;

		// Token: 0x02001CE6 RID: 7398
		private class ReferenceVisitor : ScopedReadOnlyAstVisitor<IMemberLetPartitionKey>
		{
			// Token: 0x0600B8C1 RID: 47297 RVA: 0x0025705C File Offset: 0x0025525C
			public ReferenceVisitor(MemberLetPartitionedDocument memberLetPartitionedDocument)
			{
				this.memberLetPartitionedDocument = memberLetPartitionedDocument;
				this.sectionsPartitionKeys = new Dictionary<string, Dictionary<string, IMemberLetPartitionKey>>();
				foreach (ISectionDocument sectionDocument in memberLetPartitionedDocument.ParseSections().Values)
				{
					Dictionary<string, IMemberLetPartitionKey> dictionary = new Dictionary<string, IMemberLetPartitionKey>();
					IMemberLetPartitionKey[] array = new IMemberLetPartitionKey[sectionDocument.Section.Members.Count];
					for (int i = 0; i < array.Length; i++)
					{
						ISectionMember sectionMember = sectionDocument.Section.Members[i];
						IMemberLetPartitionKey specificPartitionKey = this.memberLetPartitionedDocument.GetSpecificPartitionKey(new MemberLetPartitionKey(sectionDocument.Section.SectionName, sectionMember.Name));
						dictionary.Add(sectionMember.Name, specificPartitionKey);
						if (sectionMember.Export)
						{
							array[i] = specificPartitionKey;
						}
					}
					this.sectionsPartitionKeys.Add(sectionDocument.Section.SectionName, dictionary);
					base.EnterScope(sectionDocument.Section.Members, array);
				}
			}

			// Token: 0x0600B8C2 RID: 47298 RVA: 0x0025718C File Offset: 0x0025538C
			public Dictionary<IPartitionKey, HashSet<IPartitionKey>> GetPartitionReferences(ISectionDocument document)
			{
				this.currentSection = document.Section.SectionName.Name;
				this.currentMember = null;
				this.currentLets = EmptyArray<string>.Instance;
				this.currentPartitionKey = null;
				this.currentPartitionReferences = null;
				this.partitionReferences = new Dictionary<IPartitionKey, HashSet<IPartitionKey>>(PartitionKeyEqualityComparer.Instance);
				Dictionary<IPartitionKey, HashSet<IPartitionKey>> dictionary;
				try
				{
					base.Visit(document);
					dictionary = this.partitionReferences;
				}
				finally
				{
					this.currentSection = null;
					this.currentMember = null;
					this.currentLets = EmptyArray<string>.Instance;
					this.currentPartitionKey = null;
					this.currentPartitionReferences = null;
					this.partitionReferences = null;
				}
				return dictionary;
			}

			// Token: 0x0600B8C3 RID: 47299 RVA: 0x00257230 File Offset: 0x00255430
			protected override void VisitExpression(IExpression expression)
			{
				string[] array = this.currentLets;
				if (expression.Kind != ExpressionKind.Let)
				{
					this.currentLets = MemberLetPartitionedDocument.ReferenceVisitor.notInALetMarker;
				}
				base.VisitExpression(expression);
				this.currentLets = array;
			}

			// Token: 0x0600B8C4 RID: 47300 RVA: 0x00257267 File Offset: 0x00255467
			protected override IList<IMemberLetPartitionKey> CreateBindings(IDeclarator declarator)
			{
				return new IMemberLetPartitionKey[declarator.Count];
			}

			// Token: 0x0600B8C5 RID: 47301 RVA: 0x00257274 File Offset: 0x00255474
			protected override void VisitModule(ISection section)
			{
				IMemberLetPartitionKey[] array = new IMemberLetPartitionKey[section.Members.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetMemberPartitionKey(section.SectionName, section.Members[i].Name);
				}
				base.EnterScope(section.Members, array);
				for (int j = 0; j < section.Members.Count; j++)
				{
					string text = this.currentMember;
					IPartitionKey partitionKey = this.currentPartitionKey;
					HashSet<IPartitionKey> hashSet = this.currentPartitionReferences;
					this.currentMember = array[j].Member;
					if (!this.partitionReferences.TryGetValue(array[j], out this.currentPartitionReferences))
					{
						this.currentPartitionKey = array[j];
						this.currentPartitionReferences = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
						this.partitionReferences.Add(array[j], this.currentPartitionReferences);
					}
					this.VisitModuleMember(section.Members[j]);
					this.currentPartitionReferences = hashSet;
					this.currentPartitionKey = partitionKey;
					this.currentMember = text;
				}
				base.ExitScope(section.Members);
			}

			// Token: 0x0600B8C6 RID: 47302 RVA: 0x00257390 File Offset: 0x00255590
			protected override void VisitLet(ILetExpression let)
			{
				IIdentifierExpression identifierExpression = let.Expression as IIdentifierExpression;
				if (this.currentLets != MemberLetPartitionedDocument.ReferenceVisitor.notInALetMarker && this.currentLets.Length < 1 && identifierExpression != null)
				{
					bool flag = false;
					IMemberLetPartitionKey[] array = new IMemberLetPartitionKey[let.Variables.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new MemberLetPartitionKey(this.currentSection, this.currentMember, this.currentLets.Add(let.Variables[i].Name.Name));
						if (identifierExpression.Name.Equals(let.Variables[i].Name))
						{
							flag = true;
						}
					}
					if (flag)
					{
						base.EnterScope(let.Variables, array);
						for (int j = 0; j < let.Variables.Count; j++)
						{
							string[] array2 = this.currentLets;
							IPartitionKey partitionKey = this.currentPartitionKey;
							HashSet<IPartitionKey> hashSet = this.currentPartitionReferences;
							this.currentLets = (string[])array[j].Lets;
							if (!this.partitionReferences.TryGetValue(array[j], out this.currentPartitionReferences))
							{
								this.currentPartitionKey = array[j];
								this.currentPartitionReferences = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
								this.partitionReferences.Add(array[j], this.currentPartitionReferences);
							}
							this.VisitInitializer(let.Variables[j]);
							this.currentPartitionReferences = hashSet;
							this.currentPartitionKey = partitionKey;
							this.currentLets = array2;
						}
						this.VisitExpression(let.Expression);
						base.ExitScope(let.Variables);
						return;
					}
				}
				this.currentLets = MemberLetPartitionedDocument.ReferenceVisitor.notInALetMarker;
				base.VisitLet(let);
			}

			// Token: 0x0600B8C7 RID: 47303 RVA: 0x00257548 File Offset: 0x00255748
			protected override void VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
			{
				IMemberLetPartitionKey memberPartitionKey = this.GetMemberPartitionKey(sectionIdentifier.Section, sectionIdentifier.Name);
				if (memberPartitionKey != null && !memberPartitionKey.Equals(this.currentPartitionKey))
				{
					this.currentPartitionReferences.Add(memberPartitionKey);
				}
			}

			// Token: 0x0600B8C8 RID: 47304 RVA: 0x00257590 File Offset: 0x00255790
			protected override void VisitIdentifier(IIdentifierExpression identifier)
			{
				IMemberLetPartitionKey memberLetPartitionKey;
				if (base.TryGetValue(identifier.Name, identifier.IsInclusive, out memberLetPartitionKey) && memberLetPartitionKey != null && !memberLetPartitionKey.Equals(this.currentPartitionKey))
				{
					this.currentPartitionReferences.Add(memberLetPartitionKey);
				}
			}

			// Token: 0x0600B8C9 RID: 47305 RVA: 0x002575D4 File Offset: 0x002557D4
			private IMemberLetPartitionKey GetMemberPartitionKey(string section, string member)
			{
				Dictionary<string, IMemberLetPartitionKey> dictionary;
				IMemberLetPartitionKey memberLetPartitionKey;
				if (this.sectionsPartitionKeys.TryGetValue(section, out dictionary) && dictionary.TryGetValue(member, out memberLetPartitionKey))
				{
					return memberLetPartitionKey;
				}
				return null;
			}

			// Token: 0x04005DF9 RID: 24057
			private static readonly string[] notInALetMarker = new string[0];

			// Token: 0x04005DFA RID: 24058
			private readonly MemberLetPartitionedDocument memberLetPartitionedDocument;

			// Token: 0x04005DFB RID: 24059
			private readonly Dictionary<string, Dictionary<string, IMemberLetPartitionKey>> sectionsPartitionKeys;

			// Token: 0x04005DFC RID: 24060
			private string currentSection;

			// Token: 0x04005DFD RID: 24061
			private string currentMember;

			// Token: 0x04005DFE RID: 24062
			private string[] currentLets;

			// Token: 0x04005DFF RID: 24063
			private IPartitionKey currentPartitionKey;

			// Token: 0x04005E00 RID: 24064
			private HashSet<IPartitionKey> currentPartitionReferences;

			// Token: 0x04005E01 RID: 24065
			private Dictionary<IPartitionKey, HashSet<IPartitionKey>> partitionReferences;
		}

		// Token: 0x02001CE7 RID: 7399
		private class DocumentKey : IEquatable<MemberLetPartitionedDocument.DocumentKey>
		{
			// Token: 0x0600B8CB RID: 47307 RVA: 0x0025760C File Offset: 0x0025580C
			public static bool TryCreate(ISectionDocument document, out MemberLetPartitionedDocument.DocumentKey documentKey)
			{
				if (document.Host is ICacheableDocumentHost)
				{
					documentKey = new MemberLetPartitionedDocument.DocumentKey(document);
					return true;
				}
				documentKey = null;
				return false;
			}

			// Token: 0x0600B8CC RID: 47308 RVA: 0x00257629 File Offset: 0x00255829
			private DocumentKey(ISectionDocument document)
			{
				this.document = document;
			}

			// Token: 0x0600B8CD RID: 47309 RVA: 0x00257638 File Offset: 0x00255838
			public bool Equals(MemberLetPartitionedDocument.DocumentKey other)
			{
				return other != null && other.document == this.document;
			}

			// Token: 0x0600B8CE RID: 47310 RVA: 0x0025764D File Offset: 0x0025584D
			public override bool Equals(object other)
			{
				return this.Equals(other as MemberLetPartitionedDocument.DocumentKey);
			}

			// Token: 0x0600B8CF RID: 47311 RVA: 0x0025765B File Offset: 0x0025585B
			public override int GetHashCode()
			{
				return this.document.GetHashCode();
			}

			// Token: 0x04005E02 RID: 24066
			private readonly ISectionDocument document;
		}
	}
}
