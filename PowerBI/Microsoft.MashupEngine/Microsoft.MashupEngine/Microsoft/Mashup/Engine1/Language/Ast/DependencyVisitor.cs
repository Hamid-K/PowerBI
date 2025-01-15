using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018A4 RID: 6308
	internal class DependencyVisitor : AstVisitor
	{
		// Token: 0x0600A022 RID: 40994 RVA: 0x002114E8 File Offset: 0x0020F6E8
		public static IDictionary<DocumentRange, IList<DocumentRangePair>> Visit(IList<IDocument> documents, IList<DocumentRange> identifiers)
		{
			Dictionary<DocumentRange, IList<DocumentRangePair>> dictionary = new Dictionary<DocumentRange, IList<DocumentRangePair>>();
			foreach (DocumentRange documentRange in identifiers)
			{
				dictionary.Add(documentRange, new List<DocumentRangePair>());
			}
			IEnumerable<KeyValuePair<DocumentRange, IIdentifierBinding>> enumerable = BindingVisitor.Bind(documents);
			Dictionary<DocumentRange, DocumentRange> dictionary2 = new Dictionary<DocumentRange, DocumentRange>();
			foreach (KeyValuePair<DocumentRange, IIdentifierBinding> keyValuePair in enumerable)
			{
				if (dictionary.ContainsKey(keyValuePair.Key))
				{
					foreach (DocumentRange documentRange2 in keyValuePair.Value.References)
					{
						if (!dictionary2.ContainsKey(documentRange2))
						{
							dictionary2.Add(documentRange2, keyValuePair.Key);
						}
					}
				}
			}
			new DependencyVisitor(dictionary2, dictionary).Visit(documents);
			return dictionary;
		}

		// Token: 0x0600A023 RID: 40995 RVA: 0x002115F0 File Offset: 0x0020F7F0
		private DependencyVisitor(IDictionary<DocumentRange, DocumentRange> references, Dictionary<DocumentRange, IList<DocumentRangePair>> dependencies)
		{
			this.references = references;
			this.dependencies = dependencies;
			this.stack = new Stack<DocumentRange>();
		}

		// Token: 0x0600A024 RID: 40996 RVA: 0x00211611 File Offset: 0x0020F811
		protected override VariableInitializer VisitInitializer(VariableInitializer member)
		{
			this.stack.Push(new DocumentRange(this.document, member.Name));
			base.VisitInitializer(member);
			this.stack.Pop();
			return member;
		}

		// Token: 0x0600A025 RID: 40997 RVA: 0x00211645 File Offset: 0x0020F845
		protected override ISectionMember VisitModuleMember(ISectionMember moduleMember)
		{
			this.stack.Push(new DocumentRange(this.document, moduleMember.Name));
			base.VisitModuleMember(moduleMember);
			this.stack.Pop();
			return moduleMember;
		}

		// Token: 0x0600A026 RID: 40998 RVA: 0x00211678 File Offset: 0x0020F878
		private void AddDependency(IExpression reference)
		{
			if (this.stack.Count != 0)
			{
				DocumentRange documentRange = new DocumentRange(this.document, reference);
				DocumentRange documentRange2;
				if (this.references.TryGetValue(documentRange, out documentRange2))
				{
					foreach (DocumentRange documentRange3 in this.stack)
					{
						if (this.dependencies.ContainsKey(documentRange3))
						{
							this.dependencies[documentRange2].Add(new DocumentRangePair(documentRange3, documentRange));
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600A027 RID: 40999 RVA: 0x00211718 File Offset: 0x0020F918
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			this.AddDependency(identifier);
			return base.VisitIdentifier(identifier);
		}

		// Token: 0x0600A028 RID: 41000 RVA: 0x00211728 File Offset: 0x0020F928
		protected override IExpression VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
		{
			this.AddDependency(sectionIdentifier);
			return base.VisitSectionIdentifier(sectionIdentifier);
		}

		// Token: 0x0600A029 RID: 41001 RVA: 0x00211738 File Offset: 0x0020F938
		public void Visit(IList<IDocument> documents)
		{
			foreach (IDocument document in documents)
			{
				this.document = document;
				base.Visit(document);
				this.document = null;
			}
		}

		// Token: 0x040053D0 RID: 21456
		private IDictionary<DocumentRange, DocumentRange> references;

		// Token: 0x040053D1 RID: 21457
		private Dictionary<DocumentRange, IList<DocumentRangePair>> dependencies;

		// Token: 0x040053D2 RID: 21458
		private IDocument document;

		// Token: 0x040053D3 RID: 21459
		private Stack<DocumentRange> stack;
	}
}
