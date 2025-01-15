using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x02002901 RID: 10497
	[ChildElementInfo(typeof(Counsel))]
	[ChildElementInfo(typeof(Director))]
	[ChildElementInfo(typeof(Writer))]
	[ChildElementInfo(typeof(Artist))]
	[ChildElementInfo(typeof(Author))]
	[ChildElementInfo(typeof(BookAuthor))]
	[ChildElementInfo(typeof(Compiler))]
	[ChildElementInfo(typeof(Composer))]
	[ChildElementInfo(typeof(Conductor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Editor))]
	[ChildElementInfo(typeof(Interviewee))]
	[ChildElementInfo(typeof(Interviewer))]
	[ChildElementInfo(typeof(Inventor))]
	[ChildElementInfo(typeof(Performer))]
	[ChildElementInfo(typeof(ProducerName))]
	[ChildElementInfo(typeof(Translator))]
	internal class AuthorList : OpenXmlCompositeElement
	{
		// Token: 0x170069AE RID: 27054
		// (get) Token: 0x06014B16 RID: 84758 RVA: 0x003155C5 File Offset: 0x003137C5
		public override string LocalName
		{
			get
			{
				return "Author";
			}
		}

		// Token: 0x170069AF RID: 27055
		// (get) Token: 0x06014B17 RID: 84759 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170069B0 RID: 27056
		// (get) Token: 0x06014B18 RID: 84760 RVA: 0x00315616 File Offset: 0x00313816
		internal override int ElementTypeId
		{
			get
			{
				return 10783;
			}
		}

		// Token: 0x06014B19 RID: 84761 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014B1A RID: 84762 RVA: 0x00293ECF File Offset: 0x002920CF
		public AuthorList()
		{
		}

		// Token: 0x06014B1B RID: 84763 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AuthorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B1C RID: 84764 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AuthorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B1D RID: 84765 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AuthorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014B1E RID: 84766 RVA: 0x00315620 File Offset: 0x00313820
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (9 == namespaceId && "Artist" == name)
			{
				return new Artist();
			}
			if (9 == namespaceId && "Author" == name)
			{
				return new Author();
			}
			if (9 == namespaceId && "BookAuthor" == name)
			{
				return new BookAuthor();
			}
			if (9 == namespaceId && "Compiler" == name)
			{
				return new Compiler();
			}
			if (9 == namespaceId && "Composer" == name)
			{
				return new Composer();
			}
			if (9 == namespaceId && "Conductor" == name)
			{
				return new Conductor();
			}
			if (9 == namespaceId && "Counsel" == name)
			{
				return new Counsel();
			}
			if (9 == namespaceId && "Director" == name)
			{
				return new Director();
			}
			if (9 == namespaceId && "Editor" == name)
			{
				return new Editor();
			}
			if (9 == namespaceId && "Interviewee" == name)
			{
				return new Interviewee();
			}
			if (9 == namespaceId && "Interviewer" == name)
			{
				return new Interviewer();
			}
			if (9 == namespaceId && "Inventor" == name)
			{
				return new Inventor();
			}
			if (9 == namespaceId && "Performer" == name)
			{
				return new Performer();
			}
			if (9 == namespaceId && "ProducerName" == name)
			{
				return new ProducerName();
			}
			if (9 == namespaceId && "Translator" == name)
			{
				return new Translator();
			}
			if (9 == namespaceId && "Writer" == name)
			{
				return new Writer();
			}
			return null;
		}

		// Token: 0x170069B1 RID: 27057
		// (get) Token: 0x06014B1F RID: 84767 RVA: 0x003157AE File Offset: 0x003139AE
		internal override string[] ElementTagNames
		{
			get
			{
				return AuthorList.eleTagNames;
			}
		}

		// Token: 0x170069B2 RID: 27058
		// (get) Token: 0x06014B20 RID: 84768 RVA: 0x003157B5 File Offset: 0x003139B5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AuthorList.eleNamespaceIds;
			}
		}

		// Token: 0x170069B3 RID: 27059
		// (get) Token: 0x06014B21 RID: 84769 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x170069B4 RID: 27060
		// (get) Token: 0x06014B22 RID: 84770 RVA: 0x003157BC File Offset: 0x003139BC
		// (set) Token: 0x06014B23 RID: 84771 RVA: 0x003157C5 File Offset: 0x003139C5
		public Artist Artist
		{
			get
			{
				return base.GetElement<Artist>(0);
			}
			set
			{
				base.SetElement<Artist>(0, value);
			}
		}

		// Token: 0x170069B5 RID: 27061
		// (get) Token: 0x06014B24 RID: 84772 RVA: 0x003157CF File Offset: 0x003139CF
		// (set) Token: 0x06014B25 RID: 84773 RVA: 0x003157D8 File Offset: 0x003139D8
		public Author Author
		{
			get
			{
				return base.GetElement<Author>(1);
			}
			set
			{
				base.SetElement<Author>(1, value);
			}
		}

		// Token: 0x170069B6 RID: 27062
		// (get) Token: 0x06014B26 RID: 84774 RVA: 0x003157E2 File Offset: 0x003139E2
		// (set) Token: 0x06014B27 RID: 84775 RVA: 0x003157EB File Offset: 0x003139EB
		public BookAuthor BookAuthor
		{
			get
			{
				return base.GetElement<BookAuthor>(2);
			}
			set
			{
				base.SetElement<BookAuthor>(2, value);
			}
		}

		// Token: 0x170069B7 RID: 27063
		// (get) Token: 0x06014B28 RID: 84776 RVA: 0x003157F5 File Offset: 0x003139F5
		// (set) Token: 0x06014B29 RID: 84777 RVA: 0x003157FE File Offset: 0x003139FE
		public Compiler Compiler
		{
			get
			{
				return base.GetElement<Compiler>(3);
			}
			set
			{
				base.SetElement<Compiler>(3, value);
			}
		}

		// Token: 0x170069B8 RID: 27064
		// (get) Token: 0x06014B2A RID: 84778 RVA: 0x00315808 File Offset: 0x00313A08
		// (set) Token: 0x06014B2B RID: 84779 RVA: 0x00315811 File Offset: 0x00313A11
		public Composer Composer
		{
			get
			{
				return base.GetElement<Composer>(4);
			}
			set
			{
				base.SetElement<Composer>(4, value);
			}
		}

		// Token: 0x170069B9 RID: 27065
		// (get) Token: 0x06014B2C RID: 84780 RVA: 0x0031581B File Offset: 0x00313A1B
		// (set) Token: 0x06014B2D RID: 84781 RVA: 0x00315824 File Offset: 0x00313A24
		public Conductor Conductor
		{
			get
			{
				return base.GetElement<Conductor>(5);
			}
			set
			{
				base.SetElement<Conductor>(5, value);
			}
		}

		// Token: 0x170069BA RID: 27066
		// (get) Token: 0x06014B2E RID: 84782 RVA: 0x0031582E File Offset: 0x00313A2E
		// (set) Token: 0x06014B2F RID: 84783 RVA: 0x00315837 File Offset: 0x00313A37
		public Counsel Counsel
		{
			get
			{
				return base.GetElement<Counsel>(6);
			}
			set
			{
				base.SetElement<Counsel>(6, value);
			}
		}

		// Token: 0x170069BB RID: 27067
		// (get) Token: 0x06014B30 RID: 84784 RVA: 0x00315841 File Offset: 0x00313A41
		// (set) Token: 0x06014B31 RID: 84785 RVA: 0x0031584A File Offset: 0x00313A4A
		public Director Director
		{
			get
			{
				return base.GetElement<Director>(7);
			}
			set
			{
				base.SetElement<Director>(7, value);
			}
		}

		// Token: 0x170069BC RID: 27068
		// (get) Token: 0x06014B32 RID: 84786 RVA: 0x00315854 File Offset: 0x00313A54
		// (set) Token: 0x06014B33 RID: 84787 RVA: 0x0031585D File Offset: 0x00313A5D
		public Editor Editor
		{
			get
			{
				return base.GetElement<Editor>(8);
			}
			set
			{
				base.SetElement<Editor>(8, value);
			}
		}

		// Token: 0x170069BD RID: 27069
		// (get) Token: 0x06014B34 RID: 84788 RVA: 0x00315867 File Offset: 0x00313A67
		// (set) Token: 0x06014B35 RID: 84789 RVA: 0x00315871 File Offset: 0x00313A71
		public Interviewee Interviewee
		{
			get
			{
				return base.GetElement<Interviewee>(9);
			}
			set
			{
				base.SetElement<Interviewee>(9, value);
			}
		}

		// Token: 0x170069BE RID: 27070
		// (get) Token: 0x06014B36 RID: 84790 RVA: 0x0031587C File Offset: 0x00313A7C
		// (set) Token: 0x06014B37 RID: 84791 RVA: 0x00315886 File Offset: 0x00313A86
		public Interviewer Interviewer
		{
			get
			{
				return base.GetElement<Interviewer>(10);
			}
			set
			{
				base.SetElement<Interviewer>(10, value);
			}
		}

		// Token: 0x170069BF RID: 27071
		// (get) Token: 0x06014B38 RID: 84792 RVA: 0x00315891 File Offset: 0x00313A91
		// (set) Token: 0x06014B39 RID: 84793 RVA: 0x0031589B File Offset: 0x00313A9B
		public Inventor Inventor
		{
			get
			{
				return base.GetElement<Inventor>(11);
			}
			set
			{
				base.SetElement<Inventor>(11, value);
			}
		}

		// Token: 0x170069C0 RID: 27072
		// (get) Token: 0x06014B3A RID: 84794 RVA: 0x003158A6 File Offset: 0x00313AA6
		// (set) Token: 0x06014B3B RID: 84795 RVA: 0x003158B0 File Offset: 0x00313AB0
		public Performer Performer
		{
			get
			{
				return base.GetElement<Performer>(12);
			}
			set
			{
				base.SetElement<Performer>(12, value);
			}
		}

		// Token: 0x170069C1 RID: 27073
		// (get) Token: 0x06014B3C RID: 84796 RVA: 0x003158BB File Offset: 0x00313ABB
		// (set) Token: 0x06014B3D RID: 84797 RVA: 0x003158C5 File Offset: 0x00313AC5
		public ProducerName ProducerName
		{
			get
			{
				return base.GetElement<ProducerName>(13);
			}
			set
			{
				base.SetElement<ProducerName>(13, value);
			}
		}

		// Token: 0x170069C2 RID: 27074
		// (get) Token: 0x06014B3E RID: 84798 RVA: 0x003158D0 File Offset: 0x00313AD0
		// (set) Token: 0x06014B3F RID: 84799 RVA: 0x003158DA File Offset: 0x00313ADA
		public Translator Translator
		{
			get
			{
				return base.GetElement<Translator>(14);
			}
			set
			{
				base.SetElement<Translator>(14, value);
			}
		}

		// Token: 0x170069C3 RID: 27075
		// (get) Token: 0x06014B40 RID: 84800 RVA: 0x003158E5 File Offset: 0x00313AE5
		// (set) Token: 0x06014B41 RID: 84801 RVA: 0x003158EF File Offset: 0x00313AEF
		public Writer Writer
		{
			get
			{
				return base.GetElement<Writer>(15);
			}
			set
			{
				base.SetElement<Writer>(15, value);
			}
		}

		// Token: 0x06014B42 RID: 84802 RVA: 0x003158FA File Offset: 0x00313AFA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AuthorList>(deep);
		}

		// Token: 0x04008F8D RID: 36749
		private const string tagName = "Author";

		// Token: 0x04008F8E RID: 36750
		private const byte tagNsId = 9;

		// Token: 0x04008F8F RID: 36751
		internal const int ElementTypeIdConst = 10783;

		// Token: 0x04008F90 RID: 36752
		private static readonly string[] eleTagNames = new string[]
		{
			"Artist", "Author", "BookAuthor", "Compiler", "Composer", "Conductor", "Counsel", "Director", "Editor", "Interviewee",
			"Interviewer", "Inventor", "Performer", "ProducerName", "Translator", "Writer"
		};

		// Token: 0x04008F91 RID: 36753
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9
		};
	}
}
