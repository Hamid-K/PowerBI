using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018A1 RID: 6305
	internal class BindingVisitor : ScopedReadOnlyAstVisitor<IIdentifierBinding>
	{
		// Token: 0x0600A015 RID: 40981 RVA: 0x002110E7 File Offset: 0x0020F2E7
		public static IDictionary<DocumentRange, IIdentifierBinding> Bind(IList<IDocument> documents)
		{
			return new BindingVisitor().Visit(documents);
		}

		// Token: 0x0600A016 RID: 40982 RVA: 0x002110F4 File Offset: 0x0020F2F4
		private IDictionary<DocumentRange, IIdentifierBinding> Visit(IList<IDocument> documents)
		{
			this.bindings = new Dictionary<DocumentRange, IIdentifierBinding>();
			this.sectionBindings = new Dictionary<Identifier, BindingVisitor.SectionBinding>();
			using (ScopedReadOnlyAstVisitor<IIdentifierBinding>.EnvironmentScope environmentScope = base.EnterScope())
			{
				foreach (IDocument document in documents)
				{
					if (document.Kind == DocumentKind.Section)
					{
						ISectionDocument sectionDocument = (ISectionDocument)document;
						BindingVisitor.SectionBinding sectionBinding = new BindingVisitor.SectionBinding(new BindingVisitor.IdentifierBinding(new DocumentRange(document, sectionDocument.Section.SectionName)));
						this.bindings.Add(sectionBinding.Name.Definition, sectionBinding.Name);
						foreach (ISectionMember sectionMember in sectionDocument.Section.Members)
						{
							BindingVisitor.IdentifierBinding identifierBinding = new BindingVisitor.IdentifierBinding(new DocumentRange(document, sectionMember.Name));
							this.bindings.Add(identifierBinding.Definition, identifierBinding);
							if (sectionMember.Export)
							{
								environmentScope.Add(sectionMember.Name, identifierBinding);
							}
							if (!sectionBinding.Members.ContainsKey(sectionMember.Name))
							{
								sectionBinding.Members.Add(sectionMember.Name, identifierBinding);
							}
						}
						this.sectionBindings.Add(sectionDocument.Section.SectionName, sectionBinding);
					}
				}
				foreach (IDocument document2 in documents)
				{
					this.document = document2;
					this.VisitDocument(document2);
					this.document = null;
				}
			}
			return this.bindings;
		}

		// Token: 0x0600A017 RID: 40983 RVA: 0x00211300 File Offset: 0x0020F500
		protected override IList<IIdentifierBinding> CreateBindings(IDeclarator members)
		{
			BindingVisitor.IdentifierBinding[] array = new BindingVisitor.IdentifierBinding[members.Count];
			for (int i = 0; i < array.Length; i++)
			{
				BindingVisitor.IdentifierBinding identifierBinding = new BindingVisitor.IdentifierBinding(new DocumentRange(this.document, members[i]));
				array[i] = identifierBinding;
				if (!members[i].Range.IsNull)
				{
					this.bindings.Add(identifierBinding.Definition, identifierBinding);
				}
			}
			return array;
		}

		// Token: 0x0600A018 RID: 40984 RVA: 0x0021136C File Offset: 0x0020F56C
		protected override void VisitModule(ISection section)
		{
			IIdentifierBinding[] array = new BindingVisitor.IdentifierBinding[section.Members.Count];
			IIdentifierBinding[] array2 = array;
			for (int i = 0; i < section.Members.Count; i++)
			{
				array2[i] = this.sectionBindings[section.SectionName].Members[section.Members[i].Name];
			}
			base.VisitModule(section, array2);
		}

		// Token: 0x0600A019 RID: 40985 RVA: 0x002113DC File Offset: 0x0020F5DC
		protected override void VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
		{
			BindingVisitor.SectionBinding sectionBinding;
			if (this.sectionBindings.TryGetValue(sectionIdentifier.Section, out sectionBinding))
			{
				sectionBinding.Name.References.Add(new DocumentRange(this.document, sectionIdentifier.Section));
				IIdentifierBinding identifierBinding;
				if (sectionBinding.Members.TryGetValue(sectionIdentifier.Name, out identifierBinding))
				{
					identifierBinding.References.Add(new DocumentRange(this.document, sectionIdentifier));
				}
			}
		}

		// Token: 0x0600A01A RID: 40986 RVA: 0x0021144C File Offset: 0x0020F64C
		protected override void VisitIdentifier(IIdentifierExpression identifier)
		{
			IIdentifierBinding identifierBinding;
			if (base.TryGetValue(identifier.Name, identifier.IsInclusive, out identifierBinding))
			{
				identifierBinding.References.Add(new DocumentRange(this.document, identifier.Name));
			}
		}

		// Token: 0x040053C9 RID: 21449
		private Dictionary<DocumentRange, IIdentifierBinding> bindings;

		// Token: 0x040053CA RID: 21450
		private Dictionary<Identifier, BindingVisitor.SectionBinding> sectionBindings;

		// Token: 0x040053CB RID: 21451
		private IDocument document;

		// Token: 0x020018A2 RID: 6306
		protected class IdentifierBinding : IIdentifierBinding
		{
			// Token: 0x0600A01C RID: 40988 RVA: 0x00211493 File Offset: 0x0020F693
			public IdentifierBinding(DocumentRange definition)
			{
				this.definition = definition;
				this.references = new List<DocumentRange>();
			}

			// Token: 0x1700292E RID: 10542
			// (get) Token: 0x0600A01D RID: 40989 RVA: 0x002114AD File Offset: 0x0020F6AD
			public DocumentRange Definition
			{
				get
				{
					return this.definition;
				}
			}

			// Token: 0x1700292F RID: 10543
			// (get) Token: 0x0600A01E RID: 40990 RVA: 0x002114B5 File Offset: 0x0020F6B5
			public IList<DocumentRange> References
			{
				get
				{
					return this.references;
				}
			}

			// Token: 0x040053CC RID: 21452
			private DocumentRange definition;

			// Token: 0x040053CD RID: 21453
			private IList<DocumentRange> references;
		}

		// Token: 0x020018A3 RID: 6307
		protected class SectionBinding
		{
			// Token: 0x0600A01F RID: 40991 RVA: 0x002114BD File Offset: 0x0020F6BD
			public SectionBinding(IIdentifierBinding binding)
			{
				this.binding = binding;
				this.members = new Dictionary<Identifier, IIdentifierBinding>();
			}

			// Token: 0x17002930 RID: 10544
			// (get) Token: 0x0600A020 RID: 40992 RVA: 0x002114D7 File Offset: 0x0020F6D7
			public IIdentifierBinding Name
			{
				get
				{
					return this.binding;
				}
			}

			// Token: 0x17002931 RID: 10545
			// (get) Token: 0x0600A021 RID: 40993 RVA: 0x002114DF File Offset: 0x0020F6DF
			public Dictionary<Identifier, IIdentifierBinding> Members
			{
				get
				{
					return this.members;
				}
			}

			// Token: 0x040053CE RID: 21454
			private IIdentifierBinding binding;

			// Token: 0x040053CF RID: 21455
			private Dictionary<Identifier, IIdentifierBinding> members;
		}
	}
}
