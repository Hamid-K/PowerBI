using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007BC RID: 1980
	public class Constants
	{
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000D2969 File Offset: 0x000D0B69
		public static bool IsNullable(int i)
		{
			return (i & 1) == 1;
		}

		// Token: 0x04002A0B RID: 10763
		public const string NULLID = "NULLID";

		// Token: 0x04002A0C RID: 10764
		public const int SECCHKCD_NOTSUPPORTED = 1;

		// Token: 0x04002A0D RID: 10765
		public const int SECCHKCD_ENCALG_NOTSUPPORTED = 27;

		// Token: 0x04002A0E RID: 10766
		public static readonly string EOD00000_STR = "00000";

		// Token: 0x04002A0F RID: 10767
		public static readonly string EOD02000_STR = "02000";

		// Token: 0x04002A10 RID: 10768
		public const int QRYBLKCTL_DEFAULT = 9239;

		// Token: 0x04002A11 RID: 10769
		public const int CPT_TRIPLET_TYPE = 127;

		// Token: 0x04002A12 RID: 10770
		public const int MDD_TRIPLET_TYPE = 120;

		// Token: 0x04002A13 RID: 10771
		public const int NGDA_TRIPLET_TYPE = 118;

		// Token: 0x04002A14 RID: 10772
		public const int RLO_TRIPLET_TYPE = 113;

		// Token: 0x04002A15 RID: 10773
		public const int SDA_TRIPLET_TYPE = 112;

		// Token: 0x04002A16 RID: 10774
		public const int SDA_MD_TYPE = 1;

		// Token: 0x04002A17 RID: 10775
		public const int GDA_MD_TYPE = 2;

		// Token: 0x04002A18 RID: 10776
		public const int ROW_MD_TYPE = 3;

		// Token: 0x04002A19 RID: 10777
		public const int SQLCADTA_LID = 224;

		// Token: 0x04002A1A RID: 10778
		public const int SQLDTAGRP_LID = 208;

		// Token: 0x04002A1B RID: 10779
		public const int NULL_LID = 0;

		// Token: 0x04002A1C RID: 10780
		public const int INDICATOR_NULLABLE = 0;

		// Token: 0x04002A1D RID: 10781
		public const int NULL_DATA = 255;

		// Token: 0x04002A1E RID: 10782
		public const int TYP_NULLIND = 1;

		// Token: 0x04002A1F RID: 10783
		public const int MAX_ENV_LID = 73;

		// Token: 0x04002A20 RID: 10784
		public const int MAX_VARS_IN_NGDA = 84;

		// Token: 0x04002A21 RID: 10785
		public const int FULL_NGDA_SIZE = 255;

		// Token: 0x04002A22 RID: 10786
		public const int MDD_TRIPLET_SIZE = 7;

		// Token: 0x04002A23 RID: 10787
		public const int SDA_TRIPLET_SIZE = 12;

		// Token: 0x04002A24 RID: 10788
		public const int SQLDTA_RLO_SIZE = 6;

		// Token: 0x04002A25 RID: 10789
		public const int RLO_RPT_GRP_SIZE = 3;

		// Token: 0x04002A26 RID: 10790
		public const int SQLDTAGRP_SIZE = 3;

		// Token: 0x04002A27 RID: 10791
		public const int CPT_SIZE = 3;

		// Token: 0x04002A28 RID: 10792
		public const int FDODSC_FOOTER_SIZE = 6;

		// Token: 0x04002A29 RID: 10793
		public const int SQLDTAGRP_COL_DSC_SIZE = 3;

		// Token: 0x04002A2A RID: 10794
		public const int MAX_OVERRIDES = 250;

		// Token: 0x04002A2B RID: 10795
		public const int MDD_REST_SIZE = 5;

		// Token: 0x04002A2C RID: 10796
		public const int FDOCA_MAX_PARAMETERS = 84;

		// Token: 0x04002A2D RID: 10797
		public const int DRDA_TYPE_OVERRIDE_CHAR = 82;

		// Token: 0x04002A2E RID: 10798
		public const int DRDA_TYPE_OVERRIDE_NCHAR = 80;

		// Token: 0x04002A2F RID: 10799
		public const int DRDA_TYPE_OVERRIDE_VARCHAR = 83;

		// Token: 0x04002A30 RID: 10800
		public const int DRDA_TYPE_OVERRIDE_NVARCHAR = 81;

		// Token: 0x04002A31 RID: 10801
		public static readonly byte[] SQLCADTA_MDD = new byte[] { 7, 120, 0, 5, 3, 1, 224 };

		// Token: 0x04002A32 RID: 10802
		public static readonly byte[] SQLDTA_MDD = new byte[] { 7, 120, 0, 5, 4, 1, 240 };

		// Token: 0x04002A33 RID: 10803
		public static readonly byte[] SQLDTAGRP_MDD = new byte[] { 7, 120, 0, 5, 2, 1, 208 };

		// Token: 0x04002A34 RID: 10804
		public static readonly byte[] SQLCADTA_SQLDTARD_RLO = new byte[]
		{
			9, 113, 224, 84, 0, 1, 208, 0, 1, 6,
			113, 240, 224, 0, 0
		};

		// Token: 0x04002A35 RID: 10805
		public static readonly byte[] SQLDTA_RLO = new byte[] { 6, 113, 228, 208, 0, 1 };

		// Token: 0x04002A36 RID: 10806
		public static readonly byte[] SQLDTA_RLO_ROW_WIDE = new byte[] { 6, 113, 244, 228, 0, 0 };

		// Token: 0x04002A37 RID: 10807
		public static readonly byte[] SQLDTA_CODEPAGEOVERRIDE_TRAILING = new byte[]
		{
			7, 120, 0, 5, 3, 1, 224, 9, 113, 224,
			84, 0, 1, 208, 0, 1, 7, 120, 0, 5,
			4, 1, 240, 6, 113, 240, 224, 0, 0
		};

		// Token: 0x04002A38 RID: 10808
		public static readonly byte[] OakleyGroup5P = new byte[]
		{
			227, 179, 22, 140, 13, 122, 76, 30, 188, 96,
			13, 202, 164, 179, 245, 191, 161, 70, 104, 15,
			31, 179, 122, 148, 240, 19, 230, 62, 215, 18,
			33, 198, 0
		};

		// Token: 0x04002A39 RID: 10809
		public static readonly byte[] OakleyGroup5G = new byte[]
		{
			228, 52, 234, 217, 174, 86, 226, 33, 110, 98,
			95, 236, 220, 30, 7, 207, 222, 63, 96, 20,
			145, 108, 200, 66, 68, 29, 158, 123, 31, 250,
			144, 70, 0
		};

		// Token: 0x04002A3A RID: 10810
		public static readonly byte[] OakleyGroup14P = new byte[]
		{
			121, 249, 46, 53, 36, 117, 223, 246, 145, 252,
			187, 156, 136, 148, 21, 234, 38, 43, 227, 8,
			19, 99, 79, 114, 168, 246, 28, 183, 190, 175,
			95, 196, 16, 87, 143, 231, 204, 151, 170, 14,
			20, 99, 79, 114, 168, 246, 28, 183, 190, 175,
			95, 196, 16, 87, 143, 231, 204, 151, 170, 14,
			21, 99, 79, 242, 0
		};

		// Token: 0x04002A3B RID: 10811
		public static readonly byte[] OakleyGroup14G = new byte[]
		{
			146, 154, 192, 123, 172, 186, 178, 239, 169, 124,
			100, 129, 148, 133, 246, 214, 236, 22, 253, 38,
			153, 84, 105, 7, 248, 93, 52, 114, 143, 10,
			14, 205, 145, 68, 54, 120, 27, 131, 31, 169,
			229, 73, 138, 2, 27, 165, 93, 147, 88, 160,
			69, 217, 60, 132, 183, 159, 0, 122, 198, 68,
			8, 158, 206, 232, 0
		};

		// Token: 0x04002A3C RID: 10812
		public static readonly int SQLCADTA_SQLDTARD_RLO_SIZE = Constants.SQLCADTA_SQLDTARD_RLO.Length;

		// Token: 0x04002A3D RID: 10813
		public const int QRYCLSIMP_SERVER_CHOICE = 0;

		// Token: 0x04002A3E RID: 10814
		public const int QRYCLSIMP_YES = 1;

		// Token: 0x04002A3F RID: 10815
		public const int QRYCLSIMP_NO = 2;

		// Token: 0x04002A40 RID: 10816
		public const int QRYCLSIMP_CLS_COMM = 3;

		// Token: 0x04002A41 RID: 10817
		public const int QRYCLSIMP_CLS_CUR_TYPE = 4;

		// Token: 0x04002A42 RID: 10818
		public const int QRYCLSRLS_NO = 0;

		// Token: 0x04002A43 RID: 10819
		public const int QRYCLSRLS_YES = 1;

		// Token: 0x04002A44 RID: 10820
		public const int QRYBLKFCT_NONE = 0;

		// Token: 0x04002A45 RID: 10821
		public const int QRYBLKEXA = 0;

		// Token: 0x04002A46 RID: 10822
		public const int QRYBLKFLX = 1;

		// Token: 0x04002A47 RID: 10823
		public const int OUTOVR_FIRST_CNTQRY = 1;

		// Token: 0x04002A48 RID: 10824
		public const int OUTOVR_ANY_CNTQRY = 2;

		// Token: 0x04002A49 RID: 10825
		public const int OUTOVR_NON = 3;

		// Token: 0x04002A4A RID: 10826
		public const int QRYBLKSZ_MIN = 512;

		// Token: 0x04002A4B RID: 10827
		public const int QRYBLKSZ_MAX = 10485760;

		// Token: 0x04002A4C RID: 10828
		public const int QRYROWSET_MAX = 32767;

		// Token: 0x04002A4D RID: 10829
		public const int QRYROWSET_DEFAULT = -1;

		// Token: 0x04002A4E RID: 10830
		public const int RTNEXTROW = 1;

		// Token: 0x04002A4F RID: 10831
		public const int RTNEXTALL = 2;

		// Token: 0x04002A50 RID: 10832
		public const int MAXBLKEXT_NONE = 0;

		// Token: 0x04002A51 RID: 10833
		public const int MAXBLKEXT_DEFAULT = 0;

		// Token: 0x04002A52 RID: 10834
		public const string PRODUCT_ID = "SQL11000";

		// Token: 0x04002A53 RID: 10835
		public const string PRODUCT_ID_IBMClient = "DSN11000";

		// Token: 0x04002A54 RID: 10836
		public const string QTDSQLX86 = "QTDSQLX86";

		// Token: 0x04002A55 RID: 10837
		public const string QTDSQLASC = "QTDSQLASC";

		// Token: 0x04002A56 RID: 10838
		public const string QTDSQL400 = "QTDSQL400";

		// Token: 0x04002A57 RID: 10839
		public const string QTDSQL370 = "QTDSQL370";

		// Token: 0x04002A58 RID: 10840
		public const int QRYSCRREL = 1;

		// Token: 0x04002A59 RID: 10841
		public const int QRYSCRABS = 2;

		// Token: 0x04002A5A RID: 10842
		public const int QRYSCRAFT = 3;

		// Token: 0x04002A5B RID: 10843
		public const int QRYSCRBEF = 4;

		// Token: 0x04002A5C RID: 10844
		public const int QRYSCRNXT = 5;

		// Token: 0x04002A5D RID: 10845
		public const int QRYSCRPRI = 6;

		// Token: 0x04002A5E RID: 10846
		public const int QRYSCRFST = 7;

		// Token: 0x04002A5F RID: 10847
		public const int QRYSCRLST = 8;

		// Token: 0x04002A60 RID: 10848
		public const int QRYSCRCUR = 9;

		// Token: 0x04002A61 RID: 10849
		public const int FALSE = -16;

		// Token: 0x04002A62 RID: 10850
		public const int TRUE = -15;

		// Token: 0x04002A63 RID: 10851
		public static readonly string NULL_ERR_PROC_STR = "        ";

		// Token: 0x04002A64 RID: 10852
		public static readonly string NULL_SQL_STATE_STR = "00000";

		// Token: 0x04002A65 RID: 10853
		public static readonly byte[] ERROR_D4_D6 = new byte[12];

		// Token: 0x04002A66 RID: 10854
		public static readonly string WARN_0_A_STR = "           ";

		// Token: 0x04002A67 RID: 10855
		public const byte DIAGLVL0 = 240;

		// Token: 0x04002A68 RID: 10856
		public const byte DIAGLVL1 = 241;

		// Token: 0x04002A69 RID: 10857
		public const byte DIAGLVL2 = 242;

		// Token: 0x04002A6A RID: 10858
		public const int ZERO_INDICATOR = 0;

		// Token: 0x04002A6B RID: 10859
		public const int NULLDATA = 255;

		// Token: 0x04002A6C RID: 10860
		public const int MAX_NAME = 255;

		// Token: 0x04002A6D RID: 10861
		public const int RDBNAM_LEN = 18;

		// Token: 0x04002A6E RID: 10862
		public const int VRSNAM_LEN = 255;

		// Token: 0x04002A6F RID: 10863
		public const int PRDID_MAX = 8;

		// Token: 0x04002A70 RID: 10864
		public const int RDBCOLID_LEN = 18;

		// Token: 0x04002A71 RID: 10865
		public const int PKGID_LEN = 18;

		// Token: 0x04002A72 RID: 10866
		public const int PKGCNSTKN_LEN = 8;

		// Token: 0x04002A73 RID: 10867
		public const int PKGNAMCSN_LEN = 64;

		// Token: 0x04002A74 RID: 10868
		public const int PKGNAMCT_LEN = 62;

		// Token: 0x04002A75 RID: 10869
		public const int PKGNAM_LEN = 54;

		// Token: 0x04002A76 RID: 10870
		public const int NO_CODEPOINT = -1;

		// Token: 0x04002A77 RID: 10871
		public const int EMPTY_STACK = -1;

		// Token: 0x04002A78 RID: 10872
		public const int MAX_DSS_LENGTH = 32767;

		// Token: 0x04002A79 RID: 10873
		public const int MAX_DSS_LENGTH_64K = 65534;

		// Token: 0x04002A7A RID: 10874
		public const long MAX_EXTDTA_SIZE = 9223372036854775807L;

		// Token: 0x04002A7B RID: 10875
		public const int MAX_MARKS_NESTING = 10;

		// Token: 0x04002A7C RID: 10876
		public const int LONGVARCHAR_MAX_LEN = 32700;

		// Token: 0x04002A7D RID: 10877
		public const int DSS_ID = 208;

		// Token: 0x04002A7E RID: 10878
		public const int DSS_NOCHAIN = 0;

		// Token: 0x04002A7F RID: 10879
		public const int DSSCHAIN = 64;

		// Token: 0x04002A80 RID: 10880
		public const int DSSCHAIN_ERROR_CONTINUE = 32;

		// Token: 0x04002A81 RID: 10881
		public const int DSSCHAIN_SAME_ID = 80;

		// Token: 0x04002A82 RID: 10882
		public const int DSSFMT_OBJDSS_WITHNOREPLYREQUIRED = 5;

		// Token: 0x04002A83 RID: 10883
		public const int DSSFMT_OBJDSS = 3;

		// Token: 0x04002A84 RID: 10884
		public const int DSSFMT_RPYDSS = 2;

		// Token: 0x04002A85 RID: 10885
		public const int DSSFMT_RQSDSS = 1;

		// Token: 0x04002A86 RID: 10886
		public const int CORRELATION_ID_UNKNOWN = -1;

		// Token: 0x04002A87 RID: 10887
		public const int CONTINUATION_BIT = 32768;

		// Token: 0x04002A88 RID: 10888
		public const int GDS_ID = 208;

		// Token: 0x04002A89 RID: 10889
		public const int GDSCHAIN = 64;

		// Token: 0x04002A8A RID: 10890
		public const int GDSCHAIN_SAME_ID = 80;

		// Token: 0x04002A8B RID: 10891
		public const int GDSFMT_OBJDSS = 3;

		// Token: 0x04002A8C RID: 10892
		public const int GDSFMT_RPYDSS = 2;

		// Token: 0x04002A8D RID: 10893
		public const int GDSFMT_RQSDSS = 1;

		// Token: 0x04002A8E RID: 10894
		public const string SQLSERVERNAME = "MSAS0100";

		// Token: 0x04002A8F RID: 10895
		public const int EBCDIC_ENCODING = 500;

		// Token: 0x04002A90 RID: 10896
		public const int SERVER_CCSIDSBC = 1208;

		// Token: 0x04002A91 RID: 10897
		public const int SERVER_CCSIDMBC = 1208;

		// Token: 0x04002A92 RID: 10898
		public const int SERVER_CCSIDDBC = 1200;

		// Token: 0x04002A93 RID: 10899
		public const int SERVER_CCSIDXML = 1208;

		// Token: 0x04002A94 RID: 10900
		public const int SERVER_DEFAULT_CCSID = 1208;

		// Token: 0x04002A95 RID: 10901
		public const string SERVER_DEFAULT_ENCODING = "utf-8";

		// Token: 0x04002A96 RID: 10902
		public const string CMD = "CMD:";

		// Token: 0x04002A97 RID: 10903
		public const byte SPACE_CHAR_EBCDIC = 64;

		// Token: 0x04002A98 RID: 10904
		public const byte SPACE_CHAR_UNICODE = 32;

		// Token: 0x04002A99 RID: 10905
		public const int XIDDATASIZE = 128;

		// Token: 0x04002A9A RID: 10906
		public const int XA_OK = 0;

		// Token: 0x04002A9B RID: 10907
		public const int XA_RDONLY = 3;

		// Token: 0x04002A9C RID: 10908
		public const int XA_RETRY = 4;

		// Token: 0x04002A9D RID: 10909
		public const int XA_HEURMIX = 5;

		// Token: 0x04002A9E RID: 10910
		public const int XA_HEURRB = 6;

		// Token: 0x04002A9F RID: 10911
		public const int XA_HEURCOM = 7;

		// Token: 0x04002AA0 RID: 10912
		public const int XA_HEURHAZ = 8;

		// Token: 0x04002AA1 RID: 10913
		public const int XA_NOMIGRATE = 9;

		// Token: 0x04002AA2 RID: 10914
		public const int XA_RETRY_COMMFAIL = 10;

		// Token: 0x04002AA3 RID: 10915
		public const int XA_TWOPHASE = 13;

		// Token: 0x04002AA4 RID: 10916
		public const int XA_LCSNOTSUPP = 99;

		// Token: 0x04002AA5 RID: 10917
		public const int XA_RBROLLBACK = 100;

		// Token: 0x04002AA6 RID: 10918
		public const int XA_RBCOMMFAIL = 101;

		// Token: 0x04002AA7 RID: 10919
		public const int XA_RBDEADLOCK = 102;

		// Token: 0x04002AA8 RID: 10920
		public const int XA_RBINTEGRITY = 103;

		// Token: 0x04002AA9 RID: 10921
		public const int XA_RBTIMEOUT = 106;

		// Token: 0x04002AAA RID: 10922
		public const int XA_NODISSOCIATE = 108;

		// Token: 0x04002AAB RID: 10923
		public const int XAER_RMERR = -3;

		// Token: 0x04002AAC RID: 10924
		public const int XAER_NOTA = -4;

		// Token: 0x04002AAD RID: 10925
		public const int XAER_INVAL = -5;

		// Token: 0x04002AAE RID: 10926
		public const int XAER_PROTO = -6;

		// Token: 0x04002AAF RID: 10927
		public const int XAER_RMFAIL = -7;

		// Token: 0x04002AB0 RID: 10928
		public const int XAER_DUPID = -8;

		// Token: 0x04002AB1 RID: 10929
		public const int XAER_OUTSIDE = -9;

		// Token: 0x04002AB2 RID: 10930
		public const int FORGET_TRUE = 241;

		// Token: 0x04002AB3 RID: 10931
		public const int FORGET_FALSE = 240;

		// Token: 0x04002AB4 RID: 10932
		public const int RLSCONV_DO_NOT_REUSE = 240;

		// Token: 0x04002AB5 RID: 10933
		public const int RLSCONV_TERMINATE = 241;

		// Token: 0x04002AB6 RID: 10934
		public const int RLSCONV_REUSE = 242;

		// Token: 0x04002AB7 RID: 10935
		public const int FDOCA_TYPE_FIXEDBYTES = 1;

		// Token: 0x04002AB8 RID: 10936
		public const int FDOCA_TYPE_NFIXEDBYTES = 129;

		// Token: 0x04002AB9 RID: 10937
		public const int FDOCA_TYPE_VARBYTES = 2;

		// Token: 0x04002ABA RID: 10938
		public const int FDOCA_TYPE_NVARBYTES = 130;

		// Token: 0x04002ABB RID: 10939
		public const int FDOCA_TYPE_NTBYTES = 3;

		// Token: 0x04002ABC RID: 10940
		public const int FDOCA_TYPE_NNTBYTES = 131;

		// Token: 0x04002ABD RID: 10941
		public const int FDOCA_TYPE_PSCLBYTE = 7;

		// Token: 0x04002ABE RID: 10942
		public const int FDOCA_TYPE_NPSCLBYTE = 135;

		// Token: 0x04002ABF RID: 10943
		public const int FDOCA_TYPE_FIXEDCHAR = 16;

		// Token: 0x04002AC0 RID: 10944
		public const int FDOCA_TYPE_NFIXEDCHAR = 144;

		// Token: 0x04002AC1 RID: 10945
		public const int FDOCA_TYPE_VARCHAR = 17;

		// Token: 0x04002AC2 RID: 10946
		public const int FDOCA_TYPE_NVARCHAR = 145;

		// Token: 0x04002AC3 RID: 10947
		public const int FDOCA_TYPE_NTCHAR = 20;

		// Token: 0x04002AC4 RID: 10948
		public const int FDOCA_TYPE_NNTCHAR = 148;

		// Token: 0x04002AC5 RID: 10949
		public const int FDOCA_TYPE_PSCLCHAR = 25;

		// Token: 0x04002AC6 RID: 10950
		public const int FDOCA_TYPE_NPSCLCHAR = 153;

		// Token: 0x04002AC7 RID: 10951
		public const int FDOCA_TYPE_INTEGER_BE = 35;

		// Token: 0x04002AC8 RID: 10952
		public const int FDOCA_TYPE_NINTEGER_BE = 163;

		// Token: 0x04002AC9 RID: 10953
		public const int FDOCA_TYPE_INTEGER_LE = 36;

		// Token: 0x04002ACA RID: 10954
		public const int FDOCA_TYPE_NINTEGER_LE = 164;

		// Token: 0x04002ACB RID: 10955
		public const int FDOCA_TYPE_DECIMAL = 48;

		// Token: 0x04002ACC RID: 10956
		public const int FDOCA_TYPE_NDECIMAL = 176;

		// Token: 0x04002ACD RID: 10957
		public const int FDOCA_TYPE_NUMERIC_CHAR = 50;

		// Token: 0x04002ACE RID: 10958
		public const int FDOCA_TYPE_NNUMERIC_CHAR = 178;

		// Token: 0x04002ACF RID: 10959
		public const int FDOCA_TYPE_ZDECIMAL_IBM = 51;

		// Token: 0x04002AD0 RID: 10960
		public const int FDOCA_TYPE_NZDECIMAL_IBM = 179;

		// Token: 0x04002AD1 RID: 10961
		public const int FDOCA_TYPE_ZDECIMAL = 53;

		// Token: 0x04002AD2 RID: 10962
		public const int FDOCA_TYPE_NZDECIMAL = 181;

		// Token: 0x04002AD3 RID: 10963
		public const int FDOCA_TYPE_FLOAT_370 = 64;

		// Token: 0x04002AD4 RID: 10964
		public const int FDOCA_TYPE_NFLOAT_370 = 192;

		// Token: 0x04002AD5 RID: 10965
		public const int FDOCA_TYPE_FLOAT_X86 = 71;

		// Token: 0x04002AD6 RID: 10966
		public const int FDOCA_TYPE_NFLOAT_X86 = 199;

		// Token: 0x04002AD7 RID: 10967
		public const int FDOCA_TYPE_FLOAT_IEEE = 72;

		// Token: 0x04002AD8 RID: 10968
		public const int FDOCA_TYPE_NFLOAT_IEEE = 200;

		// Token: 0x04002AD9 RID: 10969
		public const int FDOCA_TYPE_FLOAT_VAX = 73;

		// Token: 0x04002ADA RID: 10970
		public const int FDOCA_TYPE_NFLOAT_VAX = 201;

		// Token: 0x04002ADB RID: 10971
		public const int FDOCA_TYPE_LOBBYTES = 80;

		// Token: 0x04002ADC RID: 10972
		public const int FDOCA_TYPE_NLOBBYTES = 208;

		// Token: 0x04002ADD RID: 10973
		public const int FDOCA_TYPE_LOBCHAR = 81;

		// Token: 0x04002ADE RID: 10974
		public const int FDOCA_TYPE_NLOBCHAR = 209;

		// Token: 0x04002ADF RID: 10975
		public const int DRDA_TYPE_INTEGER = 2;

		// Token: 0x04002AE0 RID: 10976
		public const int DRDA_TYPE_NINTEGER = 3;

		// Token: 0x04002AE1 RID: 10977
		public const int DRDA_TYPE_SMALL = 4;

		// Token: 0x04002AE2 RID: 10978
		public const int DRDA_TYPE_NSMALL = 5;

		// Token: 0x04002AE3 RID: 10979
		public const int DRDA_TYPE_1BYTE_INT = 6;

		// Token: 0x04002AE4 RID: 10980
		public const int DRDA_TYPE_N1BYTE_INT = 7;

		// Token: 0x04002AE5 RID: 10981
		public const int DRDA_TYPE_FLOAT16 = 8;

		// Token: 0x04002AE6 RID: 10982
		public const int DRDA_TYPE_NFLOAT16 = 9;

		// Token: 0x04002AE7 RID: 10983
		public const int DRDA_TYPE_FLOAT8 = 10;

		// Token: 0x04002AE8 RID: 10984
		public const int DRDA_TYPE_NFLOAT8 = 11;

		// Token: 0x04002AE9 RID: 10985
		public const int DRDA_TYPE_FLOAT4 = 12;

		// Token: 0x04002AEA RID: 10986
		public const int DRDA_TYPE_NFLOAT4 = 13;

		// Token: 0x04002AEB RID: 10987
		public const int DRDA_TYPE_DECIMAL = 14;

		// Token: 0x04002AEC RID: 10988
		public const int DRDA_TYPE_NDECIMAL = 15;

		// Token: 0x04002AED RID: 10989
		public const int DRDA_TYPE_ZDECIMAL = 16;

		// Token: 0x04002AEE RID: 10990
		public const int DRDA_TYPE_NZDECIMAL = 17;

		// Token: 0x04002AEF RID: 10991
		public const int DRDA_TYPE_NUMERIC_CHAR = 18;

		// Token: 0x04002AF0 RID: 10992
		public const int DRDA_TYPE_NNUMERIC_CHAR = 19;

		// Token: 0x04002AF1 RID: 10993
		public const int DRDA_TYPE_RSET_LOC = 20;

		// Token: 0x04002AF2 RID: 10994
		public const int DRDA_TYPE_NRSET_LOC = 21;

		// Token: 0x04002AF3 RID: 10995
		public const int DRDA_TYPE_INTEGER8 = 22;

		// Token: 0x04002AF4 RID: 10996
		public const int DRDA_TYPE_NINTEGER8 = 23;

		// Token: 0x04002AF5 RID: 10997
		public const int DRDA_TYPE_LOBLOC = 24;

		// Token: 0x04002AF6 RID: 10998
		public const int DRDA_TYPE_NLOBLOC = 25;

		// Token: 0x04002AF7 RID: 10999
		public const int DRDA_TYPE_CLOBLOC = 26;

		// Token: 0x04002AF8 RID: 11000
		public const int DRDA_TYPE_NCLOBLOC = 27;

		// Token: 0x04002AF9 RID: 11001
		public const int DRDA_TYPE_DBCSCLOBLOC = 28;

		// Token: 0x04002AFA RID: 11002
		public const int DRDA_TYPE_NDBCSCLOBLOC = 29;

		// Token: 0x04002AFB RID: 11003
		public const int DRDA_TYPE_ROWID = 30;

		// Token: 0x04002AFC RID: 11004
		public const int DRDA_TYPE_NROWID = 31;

		// Token: 0x04002AFD RID: 11005
		public const int DRDA_TYPE_DATE = 32;

		// Token: 0x04002AFE RID: 11006
		public const int DRDA_TYPE_NDATE = 33;

		// Token: 0x04002AFF RID: 11007
		public const int DRDA_TYPE_TIME = 34;

		// Token: 0x04002B00 RID: 11008
		public const int DRDA_TYPE_NTIME = 35;

		// Token: 0x04002B01 RID: 11009
		public const int DRDA_TYPE_TIMESTAMP = 36;

		// Token: 0x04002B02 RID: 11010
		public const int DRDA_TYPE_NTIMESTAMP = 37;

		// Token: 0x04002B03 RID: 11011
		public const int DRDA_TYPE_FIXBYTE = 38;

		// Token: 0x04002B04 RID: 11012
		public const int DRDA_TYPE_NFIXBYTE = 39;

		// Token: 0x04002B05 RID: 11013
		public const int DRDA_TYPE_VARBYTE = 40;

		// Token: 0x04002B06 RID: 11014
		public const int DRDA_TYPE_NVARBYTE = 41;

		// Token: 0x04002B07 RID: 11015
		public const int DRDA_TYPE_LONGVARBYTE = 42;

		// Token: 0x04002B08 RID: 11016
		public const int DRDA_TYPE_NLONGVARBYTE = 43;

		// Token: 0x04002B09 RID: 11017
		public const int DRDA_TYPE_NTERMBYTE = 44;

		// Token: 0x04002B0A RID: 11018
		public const int DRDA_TYPE_NNTERMBYTE = 45;

		// Token: 0x04002B0B RID: 11019
		public const int DRDA_TYPE_CSTR = 46;

		// Token: 0x04002B0C RID: 11020
		public const int DRDA_TYPE_NCSTR = 47;

		// Token: 0x04002B0D RID: 11021
		public const int DRDA_TYPE_CHAR = 48;

		// Token: 0x04002B0E RID: 11022
		public const int DRDA_TYPE_NCHAR = 49;

		// Token: 0x04002B0F RID: 11023
		public const int DRDA_TYPE_VARCHAR = 50;

		// Token: 0x04002B10 RID: 11024
		public const int DRDA_TYPE_NVARCHAR = 51;

		// Token: 0x04002B11 RID: 11025
		public const int DRDA_TYPE_LONGVARCHAR = 52;

		// Token: 0x04002B12 RID: 11026
		public const int DRDA_TYPE_NLONGVARCHAR = 53;

		// Token: 0x04002B13 RID: 11027
		public const int DRDA_TYPE_GRAPHIC = 54;

		// Token: 0x04002B14 RID: 11028
		public const int DRDA_TYPE_NGRAPHIC = 55;

		// Token: 0x04002B15 RID: 11029
		public const int DRDA_TYPE_VARGRAPHIC = 56;

		// Token: 0x04002B16 RID: 11030
		public const int DRDA_TYPE_NVARGRAPHIC = 57;

		// Token: 0x04002B17 RID: 11031
		public const int DRDA_TYPE_LONGRAPHIC = 58;

		// Token: 0x04002B18 RID: 11032
		public const int DRDA_TYPE_NLONGRAPHIC = 59;

		// Token: 0x04002B19 RID: 11033
		public const int DRDA_TYPE_CHARMIX = 60;

		// Token: 0x04002B1A RID: 11034
		public const int DRDA_TYPE_NCHARMIX = 61;

		// Token: 0x04002B1B RID: 11035
		public const int DRDA_TYPE_VARCHARMIX = 62;

		// Token: 0x04002B1C RID: 11036
		public const int DRDA_TYPE_NVARCHARMIX = 63;

		// Token: 0x04002B1D RID: 11037
		public const int DRDA_TYPE_LONGVARCHARMIX = 64;

		// Token: 0x04002B1E RID: 11038
		public const int DRDA_TYPE_NLONGVARCHARMIX = 65;

		// Token: 0x04002B1F RID: 11039
		public const int DRDA_TYPE_CSTRMIX = 66;

		// Token: 0x04002B20 RID: 11040
		public const int DRDA_TYPE_NCSTRMIX = 67;

		// Token: 0x04002B21 RID: 11041
		public const int DRDA_TYPE_PSCLBYTE = 68;

		// Token: 0x04002B22 RID: 11042
		public const int DRDA_TYPE_NPSCLBYTE = 69;

		// Token: 0x04002B23 RID: 11043
		public const int DRDA_TYPE_LSTR = 70;

		// Token: 0x04002B24 RID: 11044
		public const int DRDA_TYPE_NLSTR = 71;

		// Token: 0x04002B25 RID: 11045
		public const int DRDA_TYPE_LSTRMIX = 72;

		// Token: 0x04002B26 RID: 11046
		public const int DRDA_TYPE_NLSTRMIX = 73;

		// Token: 0x04002B27 RID: 11047
		public const int DRDA_TYPE_SDATALINK = 76;

		// Token: 0x04002B28 RID: 11048
		public const int DRDA_TYPE_NSDATALINK = 77;

		// Token: 0x04002B29 RID: 11049
		public const int DRDA_TYPE_MDATALINK = 78;

		// Token: 0x04002B2A RID: 11050
		public const int DRDA_TYPE_NMDATALINK = 79;

		// Token: 0x04002B2B RID: 11051
		public const int DRDA_TYPE_XMLSIE = 196;

		// Token: 0x04002B2C RID: 11052
		public const int DRDA_TYPE_NXMLSIE = 197;

		// Token: 0x04002B2D RID: 11053
		public const int DRDA_TYPE_XMLSEE = 198;

		// Token: 0x04002B2E RID: 11054
		public const int DRDA_TYPE_NXMLSEE = 199;

		// Token: 0x04002B2F RID: 11055
		public const int DRDA_TYPE_LOBBYTES = 200;

		// Token: 0x04002B30 RID: 11056
		public const int DRDA_TYPE_NLOBBYTES = 201;

		// Token: 0x04002B31 RID: 11057
		public const int DRDA_TYPE_LOBCSBCS = 202;

		// Token: 0x04002B32 RID: 11058
		public const int DRDA_TYPE_NLOBCSBCS = 203;

		// Token: 0x04002B33 RID: 11059
		public const int DRDA_TYPE_LOBCDBCS = 204;

		// Token: 0x04002B34 RID: 11060
		public const int DRDA_TYPE_NLOBCDBCS = 205;

		// Token: 0x04002B35 RID: 11061
		public const int DRDA_TYPE_LOBCMIXED = 206;

		// Token: 0x04002B36 RID: 11062
		public const int DRDA_TYPE_NLOBCMIXED = 207;

		// Token: 0x04002B37 RID: 11063
		public const int DRDA_TYPE_DECFLOAT = 186;

		// Token: 0x04002B38 RID: 11064
		public const int DRDA_TYPE_NDECFLOAT = 187;

		// Token: 0x04002B39 RID: 11065
		public const int DRDA_TYPE_XMLBIN = 188;

		// Token: 0x04002B3A RID: 11066
		public const int DRDA_TYPE_NXMLBIN = 189;

		// Token: 0x04002B3B RID: 11067
		public const int DRDA_TYPE_BOOLEAN = 190;

		// Token: 0x04002B3C RID: 11068
		public const int DRDA_TYPE_NBOOLEAN = 191;

		// Token: 0x04002B3D RID: 11069
		public const int DRDA_TYPE_FIXEDBINARY = 192;

		// Token: 0x04002B3E RID: 11070
		public const int DRDA_TYPE_NFIXEDBINARY = 193;

		// Token: 0x04002B3F RID: 11071
		public const int DRDA_TYPE_VARBINARY = 194;

		// Token: 0x04002B40 RID: 11072
		public const int DRDA_TYPE_NVARBINARY = 195;

		// Token: 0x04002B41 RID: 11073
		public const int DRDA_TYPE_TIMESTAMPTIMEZONE = 183;

		// Token: 0x04002B42 RID: 11074
		public const string PERFCOUNTER_CATEGORY_NAME = "Service for DRDA";

		// Token: 0x04002B43 RID: 11075
		public const string PERFCOUNTER_CATEGORY_DESCRIPTION = "Performance counters for the Microsoft Service for DRDA";

		// Token: 0x04002B44 RID: 11076
		public const string PERFCOUNTER_ACTIVE_SESSIONS = "Active Sessions";

		// Token: 0x04002B45 RID: 11077
		public const string PERFCOUNTER_ACTIVE_REQUESTERS = "Active Requesters";

		// Token: 0x04002B46 RID: 11078
		public const string PERFCOUNTER_ACTIVE_SQL_CONNECTIONS = "Active SQL Connections";

		// Token: 0x04002B47 RID: 11079
		public const string PERFCOUNTER_BYTES_RECEIVED = "Bytes Received";

		// Token: 0x04002B48 RID: 11080
		public const string PERFCOUNTER_BYTES_RECEIVED_PER_SECOND = "Bytes Received/sec";

		// Token: 0x04002B49 RID: 11081
		public const string PERFCOUNTER_BYTES_SENT = "Bytes Sent";

		// Token: 0x04002B4A RID: 11082
		public const string PERFCOUNTER_BYTES_SENT_PER_SECOND = "Bytes Sent/sec";

		// Token: 0x04002B4B RID: 11083
		public const string PERFCOUNTER_TRANSACTIONS = "Transactions";

		// Token: 0x04002B4C RID: 11084
		public const string PERFCOUNTER_TRANSACTIONS_PER_SECOND = "Transactions/sec";

		// Token: 0x04002B4D RID: 11085
		public const string PERFCOUNTER_ACTIVE_TRANSACTIONS = "Active Transactions";

		// Token: 0x04002B4E RID: 11086
		public const string PERFCOUNTER_TRANSACTIONS_COMMITTED = "Transactions Commits";

		// Token: 0x04002B4F RID: 11087
		public const string PERFCOUNTER_TRANSACTIONS_COMMITTED_PER_SECOND = "Transactions Commits/sec";

		// Token: 0x04002B50 RID: 11088
		public const string PERFCOUNTER_TRANSACTIONS_ROLLED_BACK = "Transactions Rollbacks";

		// Token: 0x04002B51 RID: 11089
		public const string DRDA_SCHEMA_TABLE_DBALIAS = "DatabaseAliases";

		// Token: 0x04002B52 RID: 11090
		public const string DRDA_SCHEMA_TABLE_DRDATOMSSQL = "DataTypeMappingDb2toSql";

		// Token: 0x04002B53 RID: 11091
		public const string DRDA_SCHEMA_TABLE_MSSQLTODRDA = "DataTypeMappingSqltoDb2";

		// Token: 0x04002B54 RID: 11092
		public const string DRDA_SCHEMA_TABLE_MSSQLTODB2_ERROR = "SQLErrorMappingsSqlToDb2";

		// Token: 0x04002B55 RID: 11093
		public const string DRDA_SCHEMA_TABLE_PACKAGEPROCEDURE = "PackageProcedures";

		// Token: 0x04002B56 RID: 11094
		public const string CURSOR_WITH_HOLD_MARKER = "/****** CURSOR WITH HOLD ******/";

		// Token: 0x04002B57 RID: 11095
		public const string CURSOR_FOR_UPDATE_MARKER = "/****** CURSOR FOR UPDATE ******/";

		// Token: 0x04002B58 RID: 11096
		public const string HAS_OUTPUT_PARAMS_MARKER = "/****** HAS OUTPUT PARAMS ******/";

		// Token: 0x04002B59 RID: 11097
		public const string RETURN_RESULTSET_MARKER = "/****** RETURN RESULTSET ******/";

		// Token: 0x04002B5A RID: 11098
		public const string SQL_DELETE_STATEMENT_MARKER = "/****** SQL DELETE STATEMENT ******/";

		// Token: 0x04002B5B RID: 11099
		public const string SQL_UPDATE_STATEMENT_MARKER = "/****** SQL UPDATE STATEMENT ******/";

		// Token: 0x04002B5C RID: 11100
		public const string BNDOPT_MARKER_START = "/****** BNDOPT: ";

		// Token: 0x04002B5D RID: 11101
		public const string BNDOPT_MARKER_END = " ******/";

		// Token: 0x04002B5E RID: 11102
		public const string SQLSTT_MARKER_START = "/****** SQLSTT: ";

		// Token: 0x04002B5F RID: 11103
		public const string SQLSTT_MARKER_END = " ******/";

		// Token: 0x04002B60 RID: 11104
		public const string STORED_PROC_INVOKE_TYPE = "@__INVOKE_TYPE__";

		// Token: 0x04002B61 RID: 11105
		public const string STORED_PROC_FETCH_ROW_COUNT = "@__FETCH_ROW_COUNT__";

		// Token: 0x04002B62 RID: 11106
		public const string SQLSET_KEY_SQLID = "SQLID";

		// Token: 0x04002B63 RID: 11107
		public const string SQLSET_KEY_USERID = "USERID";

		// Token: 0x04002B64 RID: 11108
		public const string SQLSET_KEY_WRKSTNNAME = "WRKSTNNAME";

		// Token: 0x04002B65 RID: 11109
		public const string SQLSET_KEY_APPLNAME = "APPLNAME";

		// Token: 0x04002B66 RID: 11110
		public const string SQLSET_KEY_ACCTNG = "ACCTNG";

		// Token: 0x04002B67 RID: 11111
		public const string SQLTRANSFORM_CLR = "Clr";

		// Token: 0x04002B68 RID: 11112
		public const string SQLTRANSFORM_SERVICE = "Service";

		// Token: 0x04002B69 RID: 11113
		public static int EVENTLOG_MESSAGE_ID_FAILED_CONNECT_TO_SQLSERVER = 1011;

		// Token: 0x04002B6A RID: 11114
		public static int EVENTLOG_MESSAGE_ID_FAILED_CONNECT_TO_ESSOSERVER = 1012;

		// Token: 0x04002B6B RID: 11115
		public static int EVENTLOG_MESSAGE_ID_CONNECT_TO_REMOTE_SQLSERVER = 1013;

		// Token: 0x04002B6C RID: 11116
		public static int EVENTLOG_MESSAGE_ID_CONNECT_TO_LOCAL_SQLSERVER = 1014;

		// Token: 0x04002B6D RID: 11117
		public static int EVENTLOG_MESSAGE_ID_TCP_CONNECTION_LISTENING_ON_PORT = 1015;

		// Token: 0x04002B6E RID: 11118
		public static int EVENTLOG_MESSAGE_ID_SERVICE_MODE_PRIMARY = 1016;

		// Token: 0x04002B6F RID: 11119
		public static int EVENTLOG_MESSAGE_ID_SERVICE_MODE_PARTNER = 1017;

		// Token: 0x04002B70 RID: 11120
		public static int EVENTLOG_MESSAGE_ID_SRVLST_VALUE_CHANGED = 1018;

		// Token: 0x04002B71 RID: 11121
		public static int EVENTLOG_MESSAGE_ID_PACKAGE_PROCEDURE_CACHE_FLUSHED = 1019;

		// Token: 0x04002B72 RID: 11122
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LISTEN_ON_PORT = 1020;

		// Token: 0x04002B73 RID: 11123
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_CUSTOM_NLS_CODEPAGE = 1022;

		// Token: 0x04002B74 RID: 11124
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_DEFAULT_NLS_CODEPAGE = 1023;

		// Token: 0x04002B75 RID: 11125
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_CUSTOM_TRACE_LISTENER = 1024;

		// Token: 0x04002B76 RID: 11126
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_CUSTOM_BIND_LISTENER = 1025;

		// Token: 0x04002B77 RID: 11127
		public static int EVENTLOG_MESSAGE_ID_SERVICE_INTERNAL_ERROR = 1026;

		// Token: 0x04002B78 RID: 11128
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_TEXT_LISTENER = 1027;

		// Token: 0x04002B79 RID: 11129
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_WRITE_TO_TRACE_FILE = 1028;

		// Token: 0x04002B7A RID: 11130
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_CREATE_PERF_COUNTER = 1029;

		// Token: 0x04002B7B RID: 11131
		public static int EVENTLOG_MESSAGE_ID_SERVICE_TRACE_LISTENER_CREATED = 1030;

		// Token: 0x04002B7C RID: 11132
		public static int EVENTLOG_MESSAGE_ID_SERVICE_TRACE_FILE_CREATED_NEW = 1031;

		// Token: 0x04002B7D RID: 11133
		public static int EVENTLOG_MESSAGE_ID_SERVICE_TRACE_EXTRIES_COUNT_MAX_REACHED = 1032;

		// Token: 0x04002B7E RID: 11134
		public static int EVENTLOG_MESSAGE_ID_SERVICE_CONFIGURATION_FILE_NOT_FOUND = 1033;

		// Token: 0x04002B7F RID: 11135
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_CONFIGURATION_FILE = 1034;

		// Token: 0x04002B80 RID: 11136
		public static int EVENTLOG_MESSAGE_ID_SERVICE_CONFIGURATION_RELOADED = 1035;

		// Token: 0x04002B81 RID: 11137
		public static int EVENTLOG_MESSAGE_ID_SERVICE_CLIENT_REQUEST_REJECTED = 1036;

		// Token: 0x04002B82 RID: 11138
		public static int EVENTLOG_MESSAGE_ID_SERVICE_STOPPED = 1037;

		// Token: 0x04002B83 RID: 11139
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_START = 1038;

		// Token: 0x04002B84 RID: 11140
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_STOP = 1039;

		// Token: 0x04002B85 RID: 11141
		public static int EVENTLOG_MESSAGE_ID_SERVICE_STARTED = 1040;

		// Token: 0x04002B86 RID: 11142
		public static int EVENTLOG_MESSAGE_ID_SERVICE_TIME_SENSITIVE = 1041;

		// Token: 0x04002B87 RID: 11143
		public static int EVENTLOG_MESSAGE_ID_SERVICE_LAST_INVOKE_LIST_PROCESSED = 1042;

		// Token: 0x04002B88 RID: 11144
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_CREATE_SPECIFIED_TRACE_DIRECTORY = 1043;

		// Token: 0x04002B89 RID: 11145
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_CREATE_TRACE_FILE = 1044;

		// Token: 0x04002B8A RID: 11146
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRING_WARNING = 1045;

		// Token: 0x04002B8B RID: 11147
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_EXPIREDERROR = 1046;

		// Token: 0x04002B8C RID: 11148
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_REGKEYNOACCESS = 1047;

		// Token: 0x04002B8D RID: 11149
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_REGKEYDELETED = 1048;

		// Token: 0x04002B8E RID: 11150
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_REGKEYACCESSDENIED = 1049;

		// Token: 0x04002B8F RID: 11151
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_REGKEYOPENEDFAILURE = 1050;

		// Token: 0x04002B90 RID: 11152
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_REGKEYWRONGTYPE = 1051;

		// Token: 0x04002B91 RID: 11153
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_REGKEYWRONGVALUE = 1052;

		// Token: 0x04002B92 RID: 11154
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_REGKEYINVALIDEXPIRY = 1053;

		// Token: 0x04002B93 RID: 11155
		public static int EVENTLOG_MESSAGE_ID_SERVICE_EXPIRED_EXPIREDERRORHIS = 1054;

		// Token: 0x04002B94 RID: 11156
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOGON_EXTERNAL_USER = 1055;

		// Token: 0x04002B95 RID: 11157
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_ERRORMAPPINGSXML = 1056;

		// Token: 0x04002B96 RID: 11158
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_EVENTMESSAGESXML = 1057;

		// Token: 0x04002B97 RID: 11159
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_DB2TOMSSQLXML = 1058;

		// Token: 0x04002B98 RID: 11160
		public static int EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_LOAD_MSSQLTODB2XML = 1059;

		// Token: 0x04002B99 RID: 11161
		public static int EVENTLOG_MESSAGE_ID_ESSO_MAPPING_NOT_FOUND_FOR_EXTERNAL_USER = 1060;

		// Token: 0x04002B9A RID: 11162
		public static int EVENTLOG_MESSAGE_ID_SQLEXCEPTION = 1061;

		// Token: 0x04002B9B RID: 11163
		public static int EVENTLOG_MESSAGE_ID_ERROR_ON_TRANSACTION_LOG_FILE = 1062;

		// Token: 0x04002B9C RID: 11164
		public static int EVENTLOG_MESSAGE_ID_CERTIFICATE_EXPIRES_30_DAYS = 1063;

		// Token: 0x04002B9D RID: 11165
		public static int EVENTLOG_MESSAGE_ID_NO_CERTIFICATE = 1064;

		// Token: 0x04002B9E RID: 11166
		public static int EVENTLOG_MESSAGE_ID_MULTIPLE_CERTIFICATES = 1065;

		// Token: 0x04002B9F RID: 11167
		public static int SQLDBTYPE_COUNT = 35;

		// Token: 0x04002BA0 RID: 11168
		public static int DRDATYPE_COUNT = 208;

		// Token: 0x04002BA1 RID: 11169
		public static ushort MINSRVPRTY = 0;

		// Token: 0x04002BA2 RID: 11170
		public static ushort MAXSRVPRTY = 255;

		// Token: 0x04002BA3 RID: 11171
		public static ushort MINFAILOVERSRVPRTY = 0;

		// Token: 0x04002BA4 RID: 11172
		public static ushort MAXFAILOVERSRVPRTY = 255;

		// Token: 0x04002BA5 RID: 11173
		public static byte[] DefaultRslsetflg = new byte[] { 192 };

		// Token: 0x04002BA6 RID: 11174
		public static int MaxQueryBlockSize = 32767;

		// Token: 0x04002BA7 RID: 11175
		public static int MAX_LOB_SIZE = int.MaxValue;

		// Token: 0x04002BA8 RID: 11176
		public static int MAX_SIZE_MODE2 = 2048575;
	}
}
