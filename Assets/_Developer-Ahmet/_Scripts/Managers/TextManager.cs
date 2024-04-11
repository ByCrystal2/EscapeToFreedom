using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static Micosmo.SensorToolkit.PulseJob;
public class TextManager : MonoBehaviour
{
    List<GameText> ToiletSpeakingTexts = new List<GameText>();
    List<GameText> MainStoryFriendTexts = new List<GameText>();
    List<GameText> MainStoryKeyTexts = new List<GameText>();
    List<GameText> PuzzleOtherTexts = new List<GameText>();
    List<GameText> PuzzlesTexts = new List<GameText>();
    GameText _currentText;
    WhichStep currentStep;
    private bool _waitSetup;
    public static TextManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void TextsInit()
    {
        ToiletSpeakingTextsInit();
        MainStoryFriendTextsInit();
        MainStoryKeyTextsInit();
        PuzzleOtherTextsInit();
        PuzzlesTextsInit();
    }
    private void ToiletSpeakingTextsInit()
    {
        // STEP 1
        GameText t1 = new GameText(1, "Sonunda uyandýn! Hoþ geldin.", "Konuþan Klozet", WhosNext.Closet,WhichStep.Step1);
        GameText t2 = new GameText(2, "NE! NELER OLUYOR BURDA!", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t3 = new GameText(3, "NEREDEYÝM BEN!", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t4 = new GameText(4, "SENDE NEYÝN NESÝSÝN!!!", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t5 = new GameText(5, "ÞÞÞ! Sakin ol lütfen. Kendimi tanýtmama izin ver...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t6 = new GameText(6, "Ben Klozet. Yýllardýr öðrenciler tarafýndan kullanýldým ve bu hale geldim. Okulda garip þeyler oldu ve cansýz nesneler hayat buldu...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t7 = new GameText(7, "Durumu anlamaya çalýþýyorsun, deðil mi?", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t8 = new GameText(8, "Evet! Aklým almýyor ama! Benn...Ben...", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t9 = new GameText(9, "Sakin ol ve beni dinle. Ýþte anlatmaya devam ediyorum...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t10 = new GameText(10, "Buraya nasýl geldiðini sende bilmiyorsun anlýyorum. Fakat anlatmaya vakit yok. Kýsýtlý bir zamanýmýz var, zamanla anlatacaðým her þeyi.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t11 = new GameText(11, "Söylediklerime uyarsan burdan kurtulursun. Eðer uymazsan sonuçlarýna katlanýrsýn... Beni Anladýn mý?", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t12 = new GameText(12, "(Bu ne ve bana neden yardým ediyor...) T-Tamam anladým... ", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t13 = new GameText(13, "Güzel. Þimdi ilk görevlerini veriyorum. Baþlangýç görevlerini yaptýktan sonra arkadaþýnýn notu belirecek, onu burada (Erkekler Tuvaleti) bulacaksýn. Onu bulduktan sonra bana gel.","Konuþan Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t14 = new GameText(14, "Peki... (ona güvenmeli miyim?!?)","Sen", WhosNext.Player, WhichStep.Step1);
        GameText t15 = new GameText(15, "... (Sessizlik)", "Sen", WhosNext.Player, WhichStep.Step1);
        // STEP 1
        ToiletSpeakingTexts.Add(t1);
        ToiletSpeakingTexts.Add(t2);
        ToiletSpeakingTexts.Add(t3);
        ToiletSpeakingTexts.Add(t4);
        ToiletSpeakingTexts.Add(t5);
        ToiletSpeakingTexts.Add(t6);
        ToiletSpeakingTexts.Add(t7);
        ToiletSpeakingTexts.Add(t8);
        ToiletSpeakingTexts.Add(t9);
        ToiletSpeakingTexts.Add(t10);
        ToiletSpeakingTexts.Add(t11);
        ToiletSpeakingTexts.Add(t12);
        ToiletSpeakingTexts.Add(t13);
        ToiletSpeakingTexts.Add(t14);
        ToiletSpeakingTexts.Add(t15);

        // STEP 2
        GameText t16 = new GameText(16, "Demek arkadaþýnýn notunu buldun. Tebrik ederim!", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t17 = new GameText(17, "Arkadaþýnla daha önceden görüþtük. Bu olaylardan etkilenmeden ayrýlabilen tek kiþi...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t18 = new GameText(18, "NE! Diðer arkadaþlarým nerede? Ne oldu onlara?", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t19 = new GameText(19, "Sakin ol!", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t20 = new GameText(20, "Sana cansýz nesnelerin, hayat bulmasýndan bahsetmiþtim. Hatýrlýyor musun?", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t21 = new GameText(21, "E-Evet hatýrlýyorum.", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t22 = new GameText(22, "Canlý nesnelerde ise farklý bir durum var...","Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t23 = new GameText(23, "N-Nasýl yani?", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t24 = new GameText(24, "Normal deðiller. Asla normal deðiller! NORMAL DEÐÝLLER ANLIYOR MUSUN, DEÐÝLLER!!!!!!", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t25 = new GameText(25, "Beni korkutmaya baþlýyorsun ne oldu onlara? Sen mi yaptýn yoksa!", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t26 = new GameText(26, "Ben yerinden dahi hareket edemeyen bir klozetim! Nasýl ben yapayým!", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t27 = new GameText(27, "...", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t28 = new GameText(28, "Nota dönecek olursak, arkadaþýnla son karþýlaþmamýz da pek uzun süre konuþamadýk.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t29 = new GameText(29, "Arkasýndan birden çok not býraktýðýný söyledi ve bu da onlardan biriydi. \"Eðer benim gibi biri gelirse\" ona söylememi istedi.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t30 = new GameText(30, "Kafam çok karýþýk ama ona güveniyorum...", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t31 = new GameText(31, "Güzel. O notlarda ki ipuclarýný takip ederek çýkýþý bulabilirsin. Ben ise seni bu süreçte yönlendireceðim.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t32 = new GameText(32, "Evet, okulun çýkýþ kapýsý için gerekli olan güvenlik anahtar sayýsý 10 olmalý. Çýkýþ yolu için onlarý bulman gerekiyor. Bir tanesi arkadaþýnýn notta bahsettiði yerde, kütüphanede olabilir. Onu bu katta bulabilirsin. Baþarýlar!", "Konuþan Klozet",WhosNext.Closet,WhichStep.Step2);
        GameText t33 = new GameText(33, "Ek olarak, Tuvalet dýþýna çýkamam fakat, tuvaletler arasýnda geçiþ yapabilirim. Çift sayýlarý severim. Beni o katlarda bulabilirsin.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step2);
        // STEP 2

        ToiletSpeakingTexts.Add(t16);
        ToiletSpeakingTexts.Add(t17);
        ToiletSpeakingTexts.Add(t18);
        ToiletSpeakingTexts.Add(t19);
        ToiletSpeakingTexts.Add(t20);
        ToiletSpeakingTexts.Add(t21);
        ToiletSpeakingTexts.Add(t22);
        ToiletSpeakingTexts.Add(t23);
        ToiletSpeakingTexts.Add(t24);
        ToiletSpeakingTexts.Add(t25);
        ToiletSpeakingTexts.Add(t26);
        ToiletSpeakingTexts.Add(t27);
        ToiletSpeakingTexts.Add(t28);
        ToiletSpeakingTexts.Add(t29);
        ToiletSpeakingTexts.Add(t30);
        ToiletSpeakingTexts.Add(t31);
        ToiletSpeakingTexts.Add(t32);
        ToiletSpeakingTexts.Add(t33);


        // STEP 3
        GameText t34 = new GameText(34, "Kütühnade anahtarlý bir not daha buldum.", "Sen", WhosNext.Player, WhichStep.Step3);
        GameText t35 = new GameText(35, "Evet. O oda güvenlik odasý! Sanýrým arkadaþýnýn demek istediðini anladým. Bir sonraki güvenlik anahtarý alt katta olmalý. Alt kata inmen gerekiyor.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step3);
        GameText t36 = new GameText(36, "Peki nereden alt kata ineceðim?", "Sen", WhosNext.Player, WhichStep.Step3);
        GameText t37 = new GameText(37, "Merdivenin olduðu kýsým kilitli. Anahtarlar genelde personel odasýnda bulunur. Fakat personel odasýda kilitli. Yedek anahtarlar kamera odasýnda olabilir.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step3);
        GameText t38 = new GameText(38, "Kütüphanenin yanýnda ki odayý kastediyorsun sanýrým.", "Sen", WhosNext.Player, WhichStep.Step3);
        GameText t39 = new GameText(39, "Evet o oda! Ancak personele dikkat etmelisin. Kamera odasý ve personel odasý arasýnda sürekli dolanýr. Ýyi þanslar!", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step3);
        ToiletSpeakingTexts.Add(t34);
        ToiletSpeakingTexts.Add(t35);
        ToiletSpeakingTexts.Add(t36);
        ToiletSpeakingTexts.Add(t37);
        ToiletSpeakingTexts.Add(t38);
        ToiletSpeakingTexts.Add(t39);
        // STEP 3

        // STEP 4
        GameText t40 = new GameText(40, "8. Kat'a hoþ geldin. Görüyorum ki yardýma ihtiyacýn var.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t41 = new GameText(41, "Evet bu kata indim ama ne yapacaðýmý bilmiyorum. Herhangi bir ipucu bulamadým.", "Sen", WhosNext.Player, WhichStep.Step4);
        GameText t42 = new GameText(42, "Anladým. Sana nasýl yardým edebileceðimize bir bakalým.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t43 = new GameText(43, "...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t44 = new GameText(44, "(Ne düþünüyor acaba bu kadar?)", "Sen", WhosNext.Player, WhichStep.Step4);
        GameText t45 = new GameText(45, "Evet, þimdi hatýrladým! Hatýrladýðým þeyi sevmeyeceksin ama...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t46 = new GameText(46, "Neyi hatýrladýn?", "Sen", WhosNext.Player, WhichStep.Step4);
        GameText t47 = new GameText(47, "Bu katta uðramak istemeyeceðin bir oda var. Ancak anahtar tamda o odada...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t48 = new GameText(48, "Hatýrladýðým kadarýyla, bu katta bulman gereken notuda onlar parçaladý.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t49 = new GameText(49, "O odanýn nerede olduðunu tam hatýrlamýyorum. Fakat aklýma kýrmýzý dolaplarla dolu bir koridor geliyor.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t50 = new GameText(50, "(Verebileceðin tüm bilgiler bu kadar mý?!?) Teþekkür ederim...", "Sen", WhosNext.Player, WhichStep.Step4);
        ToiletSpeakingTexts.Add(t40);
        ToiletSpeakingTexts.Add(t41);
        ToiletSpeakingTexts.Add(t42);
        ToiletSpeakingTexts.Add(t43);
        ToiletSpeakingTexts.Add(t44);
        ToiletSpeakingTexts.Add(t45);
        ToiletSpeakingTexts.Add(t46);
        ToiletSpeakingTexts.Add(t47);
        ToiletSpeakingTexts.Add(t48);
        ToiletSpeakingTexts.Add(t49);
        ToiletSpeakingTexts.Add(t50);
        // STEP 4

        // STEP 5
        GameText t51 = new GameText(51, "Demek o personellerle dolu katý aþabildin. Tebrik ederim.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t52 = new GameText(52, "Evet çok korkutucuydu, þanslýydým", "Sen", WhosNext.Player, WhichStep.Step5);
        GameText t53 = new GameText(53, "Bu katta da o þansa ihtiyacý olacak. Seni yine zorlu bir yol bekliyor.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t54 = new GameText(54, "Yine neyden kaçmam gerekiyor yeter artýk!", "Sen", WhosNext.Player, WhichStep.Step5);
        GameText t55 = new GameText(55, "Sakin ol. Sýrada ki engelleri aþmak için metanete ihtiyacýn var.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t56 = new GameText(56, "Diðer katlarda kameralar görmüþsündür. Ancak aktif olmadýklarýný farketmiþsindir.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t57 = new GameText(57, "Evet çok garipti. Kamera odasý bile vardý ama çalýþan bir þey yoktu.", "Sen", WhosNext.Player, WhichStep.Step5);
        GameText t58 = new GameText(58, "Sana nesnelerin canlandýðýný söylemiþtim. Bazýlarý tamamen çalýþmayý durdurdu. Ama 6. kattan sonra iþler biraz deðiþiyor. Kameralar da diðer nesneler gibi canlý. Takip edip, tüm personellere anýnda bilgi verebiliyor.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t59 = new GameText(59, "Personeller ile aralarýnda garip bir iliþki var. Sanki canlý nesneler ile cansýz nesneler arasýnda bir baðlantý var gibi...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t60 = new GameText(60, "Kameralarý geçebilmek için, neyse ki gerekli olan þeyi biliyorum. Arkadaþýn bir alet bulup getirmiþti.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        
        GameText t61 = new GameText(61, "Nerde olduðunu bilmiyorum fakat notunu bulursan, onada ulaþabilirsin. Not depoda olabilir. Ýyi þanslar.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step5);
        ToiletSpeakingTexts.Add(t51);
        ToiletSpeakingTexts.Add(t52);
        ToiletSpeakingTexts.Add(t53);
        ToiletSpeakingTexts.Add(t54);
        ToiletSpeakingTexts.Add(t55);
        ToiletSpeakingTexts.Add(t56);
        ToiletSpeakingTexts.Add(t57);
        ToiletSpeakingTexts.Add(t58);
        ToiletSpeakingTexts.Add(t59);
        ToiletSpeakingTexts.Add(t60);
        ToiletSpeakingTexts.Add(t61);
        // STEP 5

        // STEP 6
        GameText t62 = new GameText(62, "Evet! Ýþte bu! Bulmuþsun. Arkadaþýnýn notunda nasýl kullanýldýðý yazmýyor... ama evet, kameralarý geçtikten sonra, anahtarlarý bulmak için güvenlik odasýna bakmak isteyebilirsin", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t63 = new GameText(63, "Bir dakika, aletin nasýl çalýþtýðýný hatýrlamam gerekiyor.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t64 = new GameText(64, "(düþünüyor...)", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t65 = new GameText(65, "Üzerinde ki beyaz düðmeye basarak çalýþtýrabiliriz. Ancak bir kere kullandýktan sonra bir kaç saniye beklemen gerekiyor.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t66 = new GameText(66, "Anladým fakat ne iþe yarýyor?", "Sen", WhosNext.Player, WhichStep.Step6);
        GameText t67 = new GameText(67, "Ah evet, ne iþe yaradýðýna gelirsek: Kameralara doðru bu cihazý konumlandýrýp, beyaz düðmeye bastýðýnda, kamerayý yaklaþýk 10 saniye etkisiz hale getirebiliyorsun.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t68 = new GameText(68, "Waow! Cidden böyle bir iþlevi mi var? Bu çok iþime yarayabilir... Bilgiler için teþekkür ederim.", "Sen", WhosNext.Player, WhichStep.Step6);
        ToiletSpeakingTexts.Add(t62);
        ToiletSpeakingTexts.Add(t63);
        ToiletSpeakingTexts.Add(t64);
        ToiletSpeakingTexts.Add(t65);
        ToiletSpeakingTexts.Add(t66);
        ToiletSpeakingTexts.Add(t67);
        ToiletSpeakingTexts.Add(t68);
        // STEP 6

        // STEP 7
        GameText t69 = new GameText(69, "Görüyorum ki labirenti geçmiþsin. Ama bu katta da seni bekleyen þeyler var.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t70 = new GameText(70, "Ne gibi þeyler?", "Sen", WhosNext.Player, WhichStep.Step6);
        GameText t71 = new GameText(71, "Biraz alýþýlmýþýn dýþý diyebiliriz. Arkadaþýnýn sýnýfýný bulman gerekiyor. Orada sana yardýmcý olacak bazý þeyler olabilir.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        
        ToiletSpeakingTexts.Add(t69);
        ToiletSpeakingTexts.Add(t70);
        ToiletSpeakingTexts.Add(t71);
        // STEP 7

        // STEP 8 2. kat
        GameText t72 = new GameText(72, "Hah hoþ geldin. Yavaþ yavaþ sona doðru yaklaþýyorsun...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t73 = new GameText(73, "Evet. Sonunda bu okuldan kurtulacaðým!", "Sen", WhosNext.Player, WhichStep.Step6);
        GameText t74 = new GameText(74, "Senin adýna seviniyorum.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t75 = new GameText(76, "Bu seninle son konuþmamýz. Diðer katta olamayacaðým maalesef.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t76 = new GameText(77, "Buraya kadar azimle geldin, seni tebrik ediyorum. Her neyse göz yaþý dökmeyi býrakalým.", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t77 = new GameText(78, "Diðer katlarýn aksine, bu katta okadar zorlanacaðýný düþünmüyorum. Bol þans. V-Ve hoþçakal...", "Konuþan Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t78 = new GameText(79, "Teþekkür ederim. Hoþçakal...", "Sen", WhosNext.Player, WhichStep.Step6);

        ToiletSpeakingTexts.Add(t72);
        ToiletSpeakingTexts.Add(t73);
        ToiletSpeakingTexts.Add(t74);
        ToiletSpeakingTexts.Add(t75);
        ToiletSpeakingTexts.Add(t76);
        ToiletSpeakingTexts.Add(t77);
        ToiletSpeakingTexts.Add(t78);

        // STEP 8

        _currentText = ToiletSpeakingTexts[0];
        currentTextString = _currentText.Text;
        currentStep = _currentText.Step;
    }
    private void MainStoryFriendTextsInit()
    {
        GameText tc_1 = new GameText(2000, "Arkadaþýndan Bir Not #1", "Burada neler oluyor hiç bir fikrim yok. Uyandýðýmda yaný baþýmda konuþan bir klozet vardý. Buradan hareket edemiyordu. Yýllardýr bu okulda olduðunu söyledi. Ama son zamanlarda bir þeyler deðiþmiþ. Beni dikkatli olmam konusunda uyardý. Uyarýlarýný dikkate alarak bilgi toplamak için yola koyuldum. Gizlice koridorlarda dolanýrken, siyah giyimli personellerin aralarýnda konuþmalarýný duyuyordum ve sesleri gittikçe yaklaþýyordu. Hemen saklanmam gerekiyordu. Gördüðüm ilk kapýdan içeriye atladým. Kafamý kaldýrdýðým ilk an, buranýn bir kütüphane olduðunu anladým. Bir not buldum. Notta bir pazarlýktan bahsediyordu. Çýkýþ yolu için bulmam gereken anahtarlar olduðunu öðrendim. Notun devamýný okuyamadan saklanmak zorunda kaldým. Pazarlýðýn ne olduðunu, neden bu yerde olduðumu bilmiyorum. Korkuyorum! Buradan kaçmama asla izin vermeyecekler. Fakat bir yolunu bulacaðým...");
        GameText tc_2 = new GameText(2001, "Arkadaþýndan Bir Not #2", "Ýlk anahtarý buldum. Diðerlerini bulmak bu kadar kolay olacakmý bilmiyorum. Anahtarlara dair hiç bir ipucu yok. Yan odada bazý sesler duydum. Klozetten gidip bir bilgi almam gerek galiba.");
        GameText tc_3 = new GameText(2002, "Arkadaþýndan Bir Not #3", "Olamaz iþim çok zor! Hangi sivrizekalý anahtarý öðrenci dolabýna koymayý akýl ederki? Neyse ki 2. koridorda ki dolaplardan birinde olduðunu biliyorum. Aranmasý gereken çok fazla dolap var. Üstüneüstük kitliler. Açabilmem için bir alete ihtiyacým var. Belki 3. koridorda dolanýrken gördüðüm depodan bir þeyler çýkar. Aramaya koyulmalýyým.");
        GameText tc_4 = new GameText(2003, "Arkadaþýndan Bir Not #4", "Hayýr olamaz! Bu katta hiç bir þey bulamadým. Üstelik her yer güvenliklerle dolu. Bir an önce alt kata gitmem gerekiyor.");
        GameText tc_5 = new GameText(2004, "Arkadaþýndan Bir Not #5","Personelleri konuþurken duydum. Þanslýysam bu katta 2 anahtar bulabilirim. Konuþan Klozetin bahsettiði canlý kameralarýda gördüm. Görünmeden geçebileceðimi düþündüm, onlarý hafife aldým. Az kalsýn yakalanýyordum. Bu depoya saklandým. Bir alet gördüm. Biraz kurcalayýnca iþe yarar bir þey olduðunu anladým. Konuþan Klozetle bunu incelememiz ve ne iþe yaradýðýný öðrenmemiz gerek.");
        GameText tc_6 = new GameText(2005, "Arkadaþýndan Bir Not #6", "Çýkýþý bulmak için dolaþtým fakat olduðum noktaya geri döndüm. Labirent gibi bir tasarýmý var. Personeller dört bir koldan dolaþýyor ve kritik noktalarý tutuyor. Acilen gizlenmem gerek...");
        GameText tc_7 = new GameText(2006, "Arkadaþýndan Bir Not #7", "AMAN TANRIM! Bu okulda neler oluyor aklým almýyor. SINIFA GÝRDÝÐÝM ANDA BANA DÖNEN O BAKIÞLARDA NEYÝN NESÝYDÝ?!? Aklýmý kaçýrmak üzereyim. Bu katta yemekhane olduðunu öðrendim klozetten. Orayý hemen araþtýrýp anahtarý bulmak zorundayým. Orda olacaðýna inanýyorum...");
        GameText tc_8 = new GameText(2007, "Arkadaþýndan Bir Not #8", "Kattaki odalarý araþtýrdým. Ancak anahtarý bir türlü bulamadým. Bir þey farkettim. Her odadan 2 adet var ve kütüphanelerden birini incelediðimde bu katýn krokisini gördüm...");
        GameText tc_9 = new GameText(2008, "Arkadaþýndan Bir Not #9", "Gördüðüm kadarýyla, bazý bilmecelerle karþý karþýyayým. Doðru cevabýn beni çýkýþa doðru götüreceðine inanýyorum.");
        GameText tc_10 = new GameText(2009, "Arkadaþýndan Bir Not #10", "");


        
        MainStoryFriendTexts.Add(tc_1);
        MainStoryFriendTexts.Add(tc_2);
        MainStoryFriendTexts.Add(tc_3);
        MainStoryFriendTexts.Add(tc_4);
        MainStoryFriendTexts.Add(tc_5);
        MainStoryFriendTexts.Add(tc_6);
        MainStoryFriendTexts.Add(tc_7);
        MainStoryFriendTexts.Add(tc_8);
        MainStoryFriendTexts.Add(tc_9);
        MainStoryFriendTexts.Add(tc_10);
    }
    public void MainStoryKeyTextsInit()
    {
        GameText tc_1 = new GameText(2100, "Güvenlik Anahtarý", "10. kat kütüphanesinde bulunan güvenlik anahtarý.");
        GameText tc_2 = new GameText(2101, "Güvenlik Anahtarý", "9. katta bulunan güvenlik anahtarý.");
        GameText tc_3 = new GameText(2102, "Güvenlik Anahtarý", "8. katta bulunan güvenlik anahtarý.");
        GameText tc_4 = new GameText(2103, "Güvenlik Anahtarý", "6. katta bulunan güvenlik anahtarý.");
        GameText tc_5 = new GameText(2104, "Güvenlik Anahtarý", "6. katta bulunan güvenlik anahtarý.");
        GameText tc_6 = new GameText(2105, "Güvenlik Anahtarý", "4. katta bulunan güvenlik anahtarý.");
        GameText tc_7 = new GameText(2106, "Güvenlik Anahtarý", "3. katta bulunan güvenlik anahtarý.");
        GameText tc_8 = new GameText(2107, "Güvenlik Anahtarý", "2. katta bulunan güvenlik anahtarý.");
        GameText tc_9 = new GameText(2108, "Güvenlik Anahtarý", "2. katta bulunan güvenlik anahtarý.");
        GameText tc_10 = new GameText(2109, "Güvenlik Anahtarý", "1. katta bulunan güvenlik anahtarý.");



        GameText tc_11 = new GameText(4000, "Personel odasý anahtarý", "Personel odasý anahtarý.");
        GameText tc_12 = new GameText(4001, "Yedek merdiven kapý anahtarý", "10. kattan çýkýþý saðlar.");

        MainStoryKeyTexts.Add(tc_1);
        MainStoryKeyTexts.Add(tc_2);
        MainStoryKeyTexts.Add(tc_3);
        MainStoryKeyTexts.Add(tc_4);
        MainStoryKeyTexts.Add(tc_5);
        MainStoryKeyTexts.Add(tc_6);
        MainStoryKeyTexts.Add(tc_7);
        MainStoryKeyTexts.Add(tc_8);
        MainStoryKeyTexts.Add(tc_9);
        MainStoryKeyTexts.Add(tc_10);

        MainStoryKeyTexts.Add(tc_11);
        MainStoryKeyTexts.Add(tc_12);
    }
    private void PuzzleOtherTextsInit()
    {
        GameText tc_1 = new GameText(3000, "Býçak", "Bir insaný ciddi þekilde yaralayabilir.");
        GameText tc_2 = new GameText(3001, "Telsiz", "Güvenlik kamerasýný iptal eder.");
        GameText tc_3 = new GameText(3002, "Crowbar", "Kilitli nesneleri açabilir.");

        PuzzleOtherTexts.Add(tc_1);
        PuzzleOtherTexts.Add(tc_2);
        PuzzleOtherTexts.Add(tc_3);

    }
    private void PuzzlesTextsInit()
    {
        PuzzleMushroomTextsInit();
        PuzzleAppleTextsInit();
        PuzzleFlowerTextsInit();
        PuzzleBookTextsInit();
    }
    private void PuzzleMushroomTextsInit()
    {
        GameText ic_500 = new GameText(1000, "3 Baþlý Mantar", "Zehirli olabilir.");
        GameText ic_501 = new GameText(1001, "Dev Baþlý Mantar.", "Lezzetli bir mantar.");
        GameText ic_502 = new GameText(1002, "Tüs Mantarý.", "Ýlaçlar için kullanýlýr.");
        GameText ic_503 = new GameText(1005, "Tüs Mantarý.", "Ýlaçlar için kullanýlýr.");
        GameText ic_504 = new GameText(1008, "3 Mor Baþlý Mantar", "Zehir yapýmýnda kullanýlýr.");

        PuzzlesTexts.Add(ic_500);
        PuzzlesTexts.Add(ic_501);
        PuzzlesTexts.Add(ic_502);
        PuzzlesTexts.Add(ic_503);
        PuzzlesTexts.Add(ic_504);
    }
    private void PuzzleAppleTextsInit()
    {

    }
    private void PuzzleBookTextsInit()
    {
        GameText ic_500 = new GameText(1006, "Avcýlýk Kitabý", "Ayýlar nasýl avlanýr?");

        PuzzlesTexts.Add(ic_500);
    }
    private void PuzzleFlowerTextsInit()
    {
        GameText ic_500 = new GameText(1003, "Tekli Lavanta", "Güzel kokar.");
        GameText ic_501 = new GameText(1004, "Çoklu Lavanta", "Böceklerle çevrilidir.");
        GameText ic_502 = new GameText(1007, "Tekli Gül", "Güzelliði ile öne çýkar.");
        PuzzlesTexts.Add(ic_500);
        PuzzlesTexts.Add(ic_501);
        PuzzlesTexts.Add(ic_502);
    }
    IEnumerator WaitAfterSpeakingEnding()
    {
        SetWaitSetup(true);
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.SetActivationSpeakingPanel(false);
        UIManager.instance.SetActivationGameTimePanel(true);
        UIManager.instance.SetActivationMainMissionPanel(true);
        if (currentStep == WhichStep.Step1)
        {
            AudioManager.instance.StartGameSource();
            TimeManager.instance.StartGameTime();
            currentStep = WhichStep.Step2;
        }
        else if (currentStep == WhichStep.Step2)
        {
            currentStep = WhichStep.Step3;
        }
        else if (currentStep == WhichStep.Step3)
        {
            currentStep = WhichStep.Step4;
        }
        else if (currentStep == WhichStep.Step4)
        {
            currentStep = WhichStep.Step5;
        }
        else if (currentStep == WhichStep.Step5)
        {
            currentStep = WhichStep.Step6;
        }
        else if (currentStep == WhichStep.Step6)
        {
            currentStep = WhichStep.Step7;
        }
        else if (currentStep == WhichStep.Step7)
        {
            currentStep = WhichStep.Step8;
        }
        if (currentStep != WhichStep.Step1 && currentStep != WhichStep.Step2)
        {
            PuzzleManager.instance.MissionComplateController.MainStoryMultipleMissionComplate(InteractPanelController.instance.GetClosetSpeakedCount(), PuzzleManager.instance.MissionComplateController.GetToiletMissionID(), ComplateType.ClosetSpeakingComplate);
            Debug.Log("Klozet konusma görevi tamamlandi. ClosetSpeakedCount => " + InteractPanelController.instance.GetClosetSpeakedCount() + " MissionID => " + PuzzleManager.instance.MissionComplateController.GetToiletMissionID() + " Speaking Step => " + currentStep);
            InteractPanelController.instance.IncreaseClosetSpeakedCount();

        }        
        PlayerManager.instance.PlayerUnlock();
        GameManager.instance.SetAllToiletIsSepakingActivation(false);
        
        
    }
    public List<GameText> GetToiletTexts()
    {
        return ToiletSpeakingTexts;
    }
    public List<GameText> GetFriendTexts()
    {
        return MainStoryFriendTexts;
    }
    public GameText GetTheNextText()
    {
        if (ToiletSpeakingTexts.Count > 0)
        {
            ToiletSpeakingTexts.Remove(ToiletSpeakingTexts[0]);
            currentTextString = ToiletSpeakingTexts[0].Text;
        }
        if (ToiletSpeakingTexts.Count > 0)
        {
            Debug.Log("Before Text => " + _currentText.Text);
            _currentText = ToiletSpeakingTexts[0];
            Debug.Log("After Text => " + _currentText.Text);
            if (currentStep != _currentText.Step)
            {
                StartCoroutine(WaitAfterSpeakingEnding());
                return null;
            }
            currentStep = _currentText.Step;
            return _currentText;
        }
        else
        {
            Debug.Log("SPEAKING END!!");
            StartCoroutine(WaitAfterSpeakingEnding());
        }
        return new GameText(9999, "Null", "Text Sayisi Bitti.", WhosNext.Player, WhichStep.Step1);
    }
    public bool GetWaitSetup()
    {
        return _waitSetup;
    }
    public void SetWaitSetup(bool _go)
    {
        _waitSetup = _go;
    }
    public GameText GetCurrentText()
    {
        return _currentText;
    }
    string currentTextString;
    public char GetTheNextLetter()
    {
        if (_currentText != null && currentTextString.Length > 0)
        {
            char nextLetter = currentTextString[0];
            currentTextString = currentTextString.Substring(1);
            return nextLetter;
        }
        return '\0';
    }
    public GameText GetFriendTextWithID(int _id)
    {
        return MainStoryFriendTexts.Where(x => x.ID == _id).SingleOrDefault();
    }
    public GameText GetKeyTextWithID(int _id)
    {
        return MainStoryKeyTexts.Where(x => x.ID == _id).SingleOrDefault();
    }
    public GameText GetPuzzleOtherTextWithID(int _id)
    {
        return PuzzleOtherTexts.Where(x => x.ID == _id).SingleOrDefault();
    }
    public GameText GetPuzzlesTextsWithID(int _id)
    {
        return PuzzlesTexts.Where(x => x.ID == _id).SingleOrDefault();        
    }
}
[System.Serializable]
public class GameText
{
    public int ID;
    public string Text;
    public string Title;
    public WhosNext Who;
    public WhichStep Step;
    public string Description;
    public GameText(int _id, string _text, string _title, WhosNext _who, WhichStep _step, string _description = "")
    {
        ID = _id;
        Text = _text;
        Title = _title;
        Who = _who;
        Step = _step;
        Description = _description;
    }
    public GameText(int _id, string _title, string _text)
    {
        ID = _id;
        Text = _text;
        Title = _title;
    }
}
public enum WhosNext
{
    Player,
    Closet,
}
public enum WhichStep
{
    Step1,
    Step2,
    Step3,
    Step4,
    Step5,
    Step6,
    Step7,
    Step8,
    Step9,
    Step10
}