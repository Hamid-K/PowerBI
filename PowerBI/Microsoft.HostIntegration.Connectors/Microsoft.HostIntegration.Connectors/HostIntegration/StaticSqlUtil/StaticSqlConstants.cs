using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A76 RID: 2678
	public class StaticSqlConstants
	{
		// Token: 0x060052FD RID: 21245 RVA: 0x00150A3C File Offset: 0x0014EC3C
		static StaticSqlConstants()
		{
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.BNDCHKEXS);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.BNDCRTCTL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.BNDEXPOPT);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.DECPRC);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.DFTRDBCOL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.DGRIOPRL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PKGATHOPT);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PKGATHRUL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.CCSIDDBC);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.CCSIDMBC);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.CCSIDSBC);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PKGDFTCST);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PKGISOLVL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PKGRPLOPT);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PKGOWNID);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PKGRPLVRS);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.PRPSTTKP);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.QRYBLKCTL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.RDBRLSOPT);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.RDBNAM);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.STTDATFMT);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.STTDECDEL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.STTSTRDEL);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.STTTIMFMT);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.TITLE);
			StaticSqlConstants.BNDOPT_CODEPOINTS.Add(BNDOPTCodePoint.VRSNAM);
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.UNKNOWN] = new Tuple<string, string>("Unknown", "UNKNOWN");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDCHKEXS] = new Tuple<string, string>("bindCheck", "BNDCHKEXS");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDEXSOPT] = new Tuple<string, string>("false", "BNDEXSOPT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDEXSRQR] = new Tuple<string, string>("true", "BNDEXSRQR");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDCRTCTL] = new Tuple<string, string>("bindAllowErrors", "BNDCRTCTL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDERRALW] = new Tuple<string, string>("Yes", "BNDERRALW");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDNERALW] = new Tuple<string, string>("No", "BNDNERALW");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDCHKONL] = new Tuple<string, string>("Validate", "BNDCHKONL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.BNDEXPOPT] = new Tuple<string, string>("bindExplain", "BNDEXPOPT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.EXPALL] = new Tuple<string, string>("ExplainAll", "EXPALL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.EXPNON] = new Tuple<string, string>("ExplainNone", "EXPNON");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.EXPYES] = new Tuple<string, string>("ExplainStatic", "EXPYES");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGATHOPT] = new Tuple<string, string>("bindAuthorizationKeep", "PKGATHOPT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGATHKP] = new Tuple<string, string>("true", "PKGATHKP");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGATHRVK] = new Tuple<string, string>("false", "PKGATHRVK");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGDFTCC] = new Tuple<string, string>("packageDefaultCcsid", "PKGDFTCC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.CCSIDDBC] = new Tuple<string, string>("packageCcsidDbc", "CCSIDDBC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.CCSIDMBC] = new Tuple<string, string>("packageCcsidMbc", "CCSIDMBC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.CCSIDSBC] = new Tuple<string, string>("packageCcsidSbc", "CCSIDSBC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGDFTCST] = new Tuple<string, string>("packageCharacterSubtype", "PKGDFTCST");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.CSTBITS] = new Tuple<string, string>("Bit", "CSTBITS");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.CSTMBCS] = new Tuple<string, string>("MBCS", "CSTMBCS");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.CSTSBCS] = new Tuple<string, string>("SBCS", "CSTSBCS");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.CSTSYSDFT] = new Tuple<string, string>("Default", "CSTSYSDFT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGISOLVL] = new Tuple<string, string>("packageIsolationLevel", "PKGISOLVL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.ISOLVLALL] = new Tuple<string, string>("RepeatableRead", "ISOLVLALL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.ISOLVLCHG] = new Tuple<string, string>("ReadUncommitted", "ISOLVLCHG");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.ISOLVLCS] = new Tuple<string, string>("ReadCommitted", "ISOLVLCS");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.ISOLVLNC] = new Tuple<string, string>("NoCommit", "ISOLVLNC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.ISOLVLRR] = new Tuple<string, string>("Serializable", "ISOLVLRR");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGRPLOPT] = new Tuple<string, string>("bindReplace", "PKGRPLOPT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGRPLALW] = new Tuple<string, string>("true", "PKGRPLALW");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGRPLNA] = new Tuple<string, string>("false", "PKGRPLNA");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.QRYBLKCTL] = new Tuple<string, string>("statementQueryProtocol", "QRYBLKCTL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.FIXROWPRC] = new Tuple<string, string>("FixedRow", "FIXROWPRC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.LMTBLKPRC] = new Tuple<string, string>("LimitedBlock", "LMTBLKPRC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.FRCFIXROW] = new Tuple<string, string>("ForceFixedRow", "FRCFIXROW");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.RDBRLSOPT] = new Tuple<string, string>("releaseDatabaseResources", "RDBRLSOPT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.RDBRLSCMM] = new Tuple<string, string>("Commit", "RDBRLSCMM");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.RDBRLSCNV] = new Tuple<string, string>("Deallocation", "RDBRLSCNV");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.STTDATFMT] = new Tuple<string, string>("statementDateFormat", "STTDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.ISODATFMT] = new Tuple<string, string>("Iso", "ISODATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.USADATFMT] = new Tuple<string, string>("Usa", "USADATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.EURDATFMT] = new Tuple<string, string>("Eur", "EURDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.JISDATFMT] = new Tuple<string, string>("Jis", "JISDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.LOCDATFMT] = new Tuple<string, string>("Local", "LOCDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DFTDATFMT] = new Tuple<string, string>("DefaultLOCDATFMT", "");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DMYBLKDATFMT] = new Tuple<string, string>("DmyBlank", "DMYBLKDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DMYCMADATFMT] = new Tuple<string, string>("DmyComma", "DMYCMADATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DMYHPNDATFMT] = new Tuple<string, string>("DmyHyphen", "DMYHPNDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DMYPRDDATFMT] = new Tuple<string, string>("DmyPeriod", "DMYPRDDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DMYSLHDATFMT] = new Tuple<string, string>("DmySlash", "DMYSLHDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.JULBLKDATFMT] = new Tuple<string, string>("JulBlank", "JULBLKDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.JULCMADATFMT] = new Tuple<string, string>("JulComma", "JULCMADATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.JULHPNDATFMT] = new Tuple<string, string>("JulHyphen", "JULHPNDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.JULPRDDATFMT] = new Tuple<string, string>("JulPeriod", "JULPRDDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.JULSLHDATFMT] = new Tuple<string, string>("JulSlash", "JULSLHDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.MDYBLKDATFMT] = new Tuple<string, string>("MdyBlank", "MDYBLKDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.MDYCMADATFMT] = new Tuple<string, string>("MdyComma", "MDYCMADATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.MDYHPNDATFMT] = new Tuple<string, string>("MdyHyphen", "MDYHPNDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.MDYPRDDATFMT] = new Tuple<string, string>("MdyPeriod", "MDYPRDDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.MDYSLHDATFMT] = new Tuple<string, string>("MdySlash", "MDYSLHDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.YMDBLKDATFMT] = new Tuple<string, string>("YmdBlank", "YMDBLKDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.YMDCMADATFMT] = new Tuple<string, string>("YmdComma", "YMDCMADATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.YMDHPNDATFMT] = new Tuple<string, string>("YmdHyphen", "YMDHPNDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.YMDPRDDATFMT] = new Tuple<string, string>("YmdPeriod", "YMDPRDDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.YMDSLHDATFMT] = new Tuple<string, string>("YmdSlash", "YMDSLHDATFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.STTDECDEL] = new Tuple<string, string>("statementDecimalDelimiter", "STTDECDEL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DECDELPRD] = new Tuple<string, string>("Period", "DECDELPRD");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DECDELCMA] = new Tuple<string, string>("Comma", "DECDELCMA");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DFTPKG] = new Tuple<string, string>("Package", "DFTPKG");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.STTSTRDEL] = new Tuple<string, string>("statementStringDelimiter", "STTSTRDEL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.STRDELAP] = new Tuple<string, string>("Apostrophe", "STRDELAP");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.STRDELDQ] = new Tuple<string, string>("DoubleQuote", "STRDELDQ");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DFTPKG] = new Tuple<string, string>("Package", "DFTPKG");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.STTTIMFMT] = new Tuple<string, string>("statementTimeFormat", "STTTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.ISOTIMFMT] = new Tuple<string, string>("Iso", "ISOTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.JISTIMFMT] = new Tuple<string, string>("Jis", "JISTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.EURTIMFMT] = new Tuple<string, string>("Eur", "EURTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.USATIMFMT] = new Tuple<string, string>("Usa", "USATIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.LOCTIMFMT] = new Tuple<string, string>("Local", "LOCTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DFTTIMFMT] = new Tuple<string, string>("DefaultDFTTIMFMT", "DFTTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.HMSBLKTIMFMT] = new Tuple<string, string>("HmsBlank", "HMSBLKTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.HMSCLNTIMFMT] = new Tuple<string, string>("HmsColon", "HMSCLNTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.HMSCMATIMFMT] = new Tuple<string, string>("HmsComma", "HMSCMATIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.HMSPRDTIMFMT] = new Tuple<string, string>("HmsPeriod", "HMSPRDTIMFMT");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PRPSTTKP] = new Tuple<string, string>("keepPreparedStatement", "PRPSTTKP");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DFTPKG] = new Tuple<string, string>("defaultPackage", "DFTPKG");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DECPRC] = new Tuple<string, string>("packageDecimalPrecision", "DECPRC");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DFTRDBCOL] = new Tuple<string, string>("defaultRdbCollection", "DFTRDBCOL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.DGRIOPRL] = new Tuple<string, string>("parallelProcessDegree", "DGRIOPRL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGATHRUL] = new Tuple<string, string>("packageExecuteAuthorization", "PKGATHRUL");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGOWNID] = new Tuple<string, string>("packageOwnerIdentifier", "PKGOWNID");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.PKGRPLVRS] = new Tuple<string, string>("bindReplaceVersion", "PKGRPLVRS");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.RDBNAM] = new Tuple<string, string>("relationalDatabaseName", "RDBNAM");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.TITLE] = new Tuple<string, string>("title", "TITLE");
			StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[BNDOPTCodePoint.VRSNAM] = new Tuple<string, string>("versionName", "VRSNAM");
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.BNDCHKEXS] = new object[]
			{
				BNDOPTCodePoint.BNDEXSOPT,
				BNDOPTCodePoint.BNDEXSRQR
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.BNDCRTCTL] = new object[]
			{
				BNDOPTCodePoint.BNDNERALW,
				BNDOPTCodePoint.BNDERRALW,
				BNDOPTCodePoint.BNDCHKONL
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.BNDEXPOPT] = new object[]
			{
				BNDOPTCodePoint.EXPALL,
				BNDOPTCodePoint.EXPNON,
				BNDOPTCodePoint.EXPYES
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.PKGATHOPT] = new object[]
			{
				BNDOPTCodePoint.PKGATHKP,
				BNDOPTCodePoint.PKGATHRVK
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.PKGATHRUL] = new object[]
			{
				new CustomOptionValue
				{
					Value = 0,
					XmlName = "Requester"
				},
				new CustomOptionValue
				{
					Value = 1,
					XmlName = "Owner"
				},
				new CustomOptionValue
				{
					Value = 2,
					XmlName = "Invoker"
				},
				new CustomOptionValue
				{
					Value = 3,
					XmlName = "Definer"
				},
				new CustomOptionValue
				{
					Value = 4,
					XmlName = "InvokerRevertToRequester"
				},
				new CustomOptionValue
				{
					Value = 5,
					XmlName = "InvokerRevertToOwner"
				},
				new CustomOptionValue
				{
					Value = 6,
					XmlName = "DefinerRevertToRequester"
				},
				new CustomOptionValue
				{
					Value = 7,
					XmlName = "DefinerRevertToOwner"
				}
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.PKGDFTCC] = new object[]
			{
				BNDOPTCodePoint.CCSIDDBC,
				BNDOPTCodePoint.CCSIDMBC,
				BNDOPTCodePoint.CCSIDSBC
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.PKGDFTCST] = new object[]
			{
				BNDOPTCodePoint.CSTSYSDFT,
				BNDOPTCodePoint.CSTBITS,
				BNDOPTCodePoint.CSTMBCS,
				BNDOPTCodePoint.CSTSBCS
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.PKGISOLVL] = new object[]
			{
				BNDOPTCodePoint.ISOLVLALL,
				BNDOPTCodePoint.ISOLVLCHG,
				BNDOPTCodePoint.ISOLVLCS,
				BNDOPTCodePoint.ISOLVLNC,
				BNDOPTCodePoint.ISOLVLRR
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.PKGRPLOPT] = new object[]
			{
				BNDOPTCodePoint.PKGRPLALW,
				BNDOPTCodePoint.PKGRPLNA
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.PRPSTTKP] = new object[]
			{
				new CustomOptionValue
				{
					Value = 240,
					XmlName = "None"
				},
				new CustomOptionValue
				{
					Value = 241,
					XmlName = "Commit"
				},
				new CustomOptionValue
				{
					Value = 242,
					XmlName = "Rollback"
				},
				new CustomOptionValue
				{
					Value = 243,
					XmlName = "All"
				}
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.QRYBLKCTL] = new object[]
			{
				BNDOPTCodePoint.FIXROWPRC,
				BNDOPTCodePoint.LMTBLKPRC,
				BNDOPTCodePoint.FRCFIXROW
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.RDBRLSOPT] = new object[]
			{
				BNDOPTCodePoint.RDBRLSCMM,
				BNDOPTCodePoint.RDBRLSCNV
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.STTDATFMT] = new object[]
			{
				BNDOPTCodePoint.ISODATFMT,
				BNDOPTCodePoint.USADATFMT,
				BNDOPTCodePoint.EURDATFMT,
				BNDOPTCodePoint.JISDATFMT,
				BNDOPTCodePoint.LOCDATFMT,
				BNDOPTCodePoint.DFTDATFMT,
				BNDOPTCodePoint.DMYBLKDATFMT,
				BNDOPTCodePoint.DMYCMADATFMT,
				BNDOPTCodePoint.DMYHPNDATFMT,
				BNDOPTCodePoint.DMYPRDDATFMT,
				BNDOPTCodePoint.DMYSLHDATFMT,
				BNDOPTCodePoint.JULBLKDATFMT,
				BNDOPTCodePoint.JULCMADATFMT,
				BNDOPTCodePoint.JULHPNDATFMT,
				BNDOPTCodePoint.JULPRDDATFMT,
				BNDOPTCodePoint.JULSLHDATFMT,
				BNDOPTCodePoint.MDYBLKDATFMT,
				BNDOPTCodePoint.MDYCMADATFMT,
				BNDOPTCodePoint.MDYHPNDATFMT,
				BNDOPTCodePoint.MDYPRDDATFMT,
				BNDOPTCodePoint.MDYSLHDATFMT,
				BNDOPTCodePoint.YMDBLKDATFMT,
				BNDOPTCodePoint.YMDCMADATFMT,
				BNDOPTCodePoint.YMDHPNDATFMT,
				BNDOPTCodePoint.YMDPRDDATFMT,
				BNDOPTCodePoint.YMDSLHDATFMT
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.STTDECDEL] = new object[]
			{
				BNDOPTCodePoint.DECDELPRD,
				BNDOPTCodePoint.DECDELCMA,
				BNDOPTCodePoint.DFTPKG,
				new CustomOptionValue
				{
					Value = 0,
					XmlName = "System"
				}
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.STTSTRDEL] = new object[]
			{
				BNDOPTCodePoint.STRDELAP,
				BNDOPTCodePoint.STRDELDQ,
				BNDOPTCodePoint.DFTPKG,
				new CustomOptionValue
				{
					Value = 0,
					XmlName = "System"
				}
			};
			StaticSqlConstants.BNDOPT_CHOICES[BNDOPTCodePoint.STTTIMFMT] = new object[]
			{
				BNDOPTCodePoint.ISOTIMFMT,
				BNDOPTCodePoint.JISTIMFMT,
				BNDOPTCodePoint.EURTIMFMT,
				BNDOPTCodePoint.USATIMFMT,
				BNDOPTCodePoint.LOCTIMFMT,
				BNDOPTCodePoint.DFTTIMFMT,
				BNDOPTCodePoint.HMSBLKTIMFMT,
				BNDOPTCodePoint.HMSCLNTIMFMT,
				BNDOPTCodePoint.HMSCMATIMFMT,
				BNDOPTCodePoint.HMSPRDTIMFMT
			};
		}

		// Token: 0x04004233 RID: 16947
		public static Dictionary<BNDOPTCodePoint, Tuple<string, string>> CODEPOINT_XML_NAME_MAPPINGS = new Dictionary<BNDOPTCodePoint, Tuple<string, string>>();

		// Token: 0x04004234 RID: 16948
		public static Hashtable BNDOPT_CHOICES = new Hashtable();

		// Token: 0x04004235 RID: 16949
		public static List<BNDOPTCodePoint> BNDOPT_CODEPOINTS = new List<BNDOPTCodePoint>();
	}
}
