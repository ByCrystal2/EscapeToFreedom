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
        GameText t1 = new GameText(1, "Sonunda uyand�n! Ho� geldin.", "Konu�an Klozet", WhosNext.Closet,WhichStep.Step1);
        GameText t2 = new GameText(2, "NE! NELER OLUYOR BURDA!", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t3 = new GameText(3, "NEREDEY�M BEN!", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t4 = new GameText(4, "SENDE NEY�N NES�S�N!!!", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t5 = new GameText(5, "���! Sakin ol l�tfen. Kendimi tan�tmama izin ver...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t6 = new GameText(6, "Ben Klozet. Y�llard�r ��renciler taraf�ndan kullan�ld�m ve bu hale geldim. Okulda garip �eyler oldu ve cans�z nesneler hayat buldu...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t7 = new GameText(7, "Durumu anlamaya �al���yorsun, de�il mi?", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t8 = new GameText(8, "Evet! Akl�m alm�yor ama! Benn...Ben...", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t9 = new GameText(9, "Sakin ol ve beni dinle. ��te anlatmaya devam ediyorum...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t10 = new GameText(10, "Buraya nas�l geldi�ini sende bilmiyorsun anl�yorum. Fakat anlatmaya vakit yok. K�s�tl� bir zaman�m�z var, zamanla anlataca��m her �eyi.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t11 = new GameText(11, "S�ylediklerime uyarsan burdan kurtulursun. E�er uymazsan sonu�lar�na katlan�rs�n... Beni Anlad�n m�?", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t12 = new GameText(12, "(Bu ne ve bana neden yard�m ediyor...) T-Tamam anlad�m... ", "Sen", WhosNext.Player, WhichStep.Step1);
        GameText t13 = new GameText(13, "G�zel. �imdi ilk g�revlerini veriyorum. Ba�lang�� g�revlerini yapt�ktan sonra arkada��n�n notu belirecek, onu burada (Erkekler Tuvaleti) bulacaks�n. Onu bulduktan sonra bana gel.","Konu�an Klozet", WhosNext.Closet, WhichStep.Step1);
        GameText t14 = new GameText(14, "Peki... (ona g�venmeli miyim?!?)","Sen", WhosNext.Player, WhichStep.Step1);
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
        GameText t16 = new GameText(16, "Demek arkada��n�n notunu buldun. Tebrik ederim!", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t17 = new GameText(17, "Arkada��nla daha �nceden g�r��t�k. Bu olaylardan etkilenmeden ayr�labilen tek ki�i...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t18 = new GameText(18, "NE! Di�er arkada�lar�m nerede? Ne oldu onlara?", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t19 = new GameText(19, "Sakin ol!", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t20 = new GameText(20, "Sana cans�z nesnelerin, hayat bulmas�ndan bahsetmi�tim. Hat�rl�yor musun?", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t21 = new GameText(21, "E-Evet hat�rl�yorum.", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t22 = new GameText(22, "Canl� nesnelerde ise farkl� bir durum var...","Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t23 = new GameText(23, "N-Nas�l yani?", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t24 = new GameText(24, "Normal de�iller. Asla normal de�iller! NORMAL DE��LLER ANLIYOR MUSUN, DE��LLER!!!!!!", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t25 = new GameText(25, "Beni korkutmaya ba�l�yorsun ne oldu onlara? Sen mi yapt�n yoksa!", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t26 = new GameText(26, "Ben yerinden dahi hareket edemeyen bir klozetim! Nas�l ben yapay�m!", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t27 = new GameText(27, "...", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t28 = new GameText(28, "Nota d�necek olursak, arkada��nla son kar��la�mam�z da pek uzun s�re konu�amad�k.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t29 = new GameText(29, "Arkas�ndan birden �ok not b�rakt���n� s�yledi ve bu da onlardan biriydi. \"E�er benim gibi biri gelirse\" ona s�ylememi istedi.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t30 = new GameText(30, "Kafam �ok kar���k ama ona g�veniyorum...", "Sen", WhosNext.Player, WhichStep.Step2);
        GameText t31 = new GameText(31, "G�zel. O notlarda ki ipuclar�n� takip ederek ��k��� bulabilirsin. Ben ise seni bu s�re�te y�nlendirece�im.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
        GameText t32 = new GameText(32, "Evet, okulun ��k�� kap�s� i�in gerekli olan g�venlik anahtar say�s� 10 olmal�. ��k�� yolu i�in onlar� bulman gerekiyor. Bir tanesi arkada��n�n notta bahsetti�i yerde, k�t�phanede olabilir. Onu bu katta bulabilirsin. Ba�ar�lar!", "Konu�an Klozet",WhosNext.Closet,WhichStep.Step2);
        GameText t33 = new GameText(33, "Ek olarak, Tuvalet d���na ��kamam fakat, tuvaletler aras�nda ge�i� yapabilirim. �ift say�lar� severim. Beni o katlarda bulabilirsin.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step2);
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
        GameText t34 = new GameText(34, "K�t�hnade anahtarl� bir not daha buldum.", "Sen", WhosNext.Player, WhichStep.Step3);
        GameText t35 = new GameText(35, "Evet. O oda g�venlik odas�! San�r�m arkada��n�n demek istedi�ini anlad�m. Bir sonraki g�venlik anahtar� alt katta olmal�. Alt kata inmen gerekiyor.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step3);
        GameText t36 = new GameText(36, "Peki nereden alt kata inece�im?", "Sen", WhosNext.Player, WhichStep.Step3);
        GameText t37 = new GameText(37, "Merdivenin oldu�u k�s�m kilitli. Anahtarlar genelde personel odas�nda bulunur. Fakat personel odas�da kilitli. Yedek anahtarlar kamera odas�nda olabilir.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step3);
        GameText t38 = new GameText(38, "K�t�phanenin yan�nda ki oday� kastediyorsun san�r�m.", "Sen", WhosNext.Player, WhichStep.Step3);
        GameText t39 = new GameText(39, "Evet o oda! Ancak personele dikkat etmelisin. Kamera odas� ve personel odas� aras�nda s�rekli dolan�r. �yi �anslar!", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step3);
        ToiletSpeakingTexts.Add(t34);
        ToiletSpeakingTexts.Add(t35);
        ToiletSpeakingTexts.Add(t36);
        ToiletSpeakingTexts.Add(t37);
        ToiletSpeakingTexts.Add(t38);
        ToiletSpeakingTexts.Add(t39);
        // STEP 3

        // STEP 4
        GameText t40 = new GameText(40, "8. Kat'a ho� geldin. G�r�yorum ki yard�ma ihtiyac�n var.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t41 = new GameText(41, "Evet bu kata indim ama ne yapaca��m� bilmiyorum. Herhangi bir ipucu bulamad�m.", "Sen", WhosNext.Player, WhichStep.Step4);
        GameText t42 = new GameText(42, "Anlad�m. Sana nas�l yard�m edebilece�imize bir bakal�m.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t43 = new GameText(43, "...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t44 = new GameText(44, "(Ne d���n�yor acaba bu kadar?)", "Sen", WhosNext.Player, WhichStep.Step4);
        GameText t45 = new GameText(45, "Evet, �imdi hat�rlad�m! Hat�rlad���m �eyi sevmeyeceksin ama...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t46 = new GameText(46, "Neyi hat�rlad�n?", "Sen", WhosNext.Player, WhichStep.Step4);
        GameText t47 = new GameText(47, "Bu katta u�ramak istemeyece�in bir oda var. Ancak anahtar tamda o odada...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t48 = new GameText(48, "Hat�rlad���m kadar�yla, bu katta bulman gereken notuda onlar par�alad�.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t49 = new GameText(49, "O odan�n nerede oldu�unu tam hat�rlam�yorum. Fakat akl�ma k�rm�z� dolaplarla dolu bir koridor geliyor.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step4);
        GameText t50 = new GameText(50, "(Verebilece�in t�m bilgiler bu kadar m�?!?) Te�ekk�r ederim...", "Sen", WhosNext.Player, WhichStep.Step4);
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
        GameText t51 = new GameText(51, "Demek o personellerle dolu kat� a�abildin. Tebrik ederim.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t52 = new GameText(52, "Evet �ok korkutucuydu, �ansl�yd�m", "Sen", WhosNext.Player, WhichStep.Step5);
        GameText t53 = new GameText(53, "Bu katta da o �ansa ihtiyac� olacak. Seni yine zorlu bir yol bekliyor.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t54 = new GameText(54, "Yine neyden ka�mam gerekiyor yeter art�k!", "Sen", WhosNext.Player, WhichStep.Step5);
        GameText t55 = new GameText(55, "Sakin ol. S�rada ki engelleri a�mak i�in metanete ihtiyac�n var.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t56 = new GameText(56, "Di�er katlarda kameralar g�rm��s�nd�r. Ancak aktif olmad�klar�n� farketmi�sindir.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t57 = new GameText(57, "Evet �ok garipti. Kamera odas� bile vard� ama �al��an bir �ey yoktu.", "Sen", WhosNext.Player, WhichStep.Step5);
        GameText t58 = new GameText(58, "Sana nesnelerin canland���n� s�ylemi�tim. Baz�lar� tamamen �al��may� durdurdu. Ama 6. kattan sonra i�ler biraz de�i�iyor. Kameralar da di�er nesneler gibi canl�. Takip edip, t�m personellere an�nda bilgi verebiliyor.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t59 = new GameText(59, "Personeller ile aralar�nda garip bir ili�ki var. Sanki canl� nesneler ile cans�z nesneler aras�nda bir ba�lant� var gibi...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
        GameText t60 = new GameText(60, "Kameralar� ge�ebilmek i�in, neyse ki gerekli olan �eyi biliyorum. Arkada��n bir alet bulup getirmi�ti.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
        
        GameText t61 = new GameText(61, "Nerde oldu�unu bilmiyorum fakat notunu bulursan, onada ula�abilirsin. Not depoda olabilir. �yi �anslar.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step5);
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
        GameText t62 = new GameText(62, "Evet! ��te bu! Bulmu�sun. Arkada��n�n notunda nas�l kullan�ld��� yazm�yor... ama evet, kameralar� ge�tikten sonra, anahtarlar� bulmak i�in g�venlik odas�na bakmak isteyebilirsin", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t63 = new GameText(63, "Bir dakika, aletin nas�l �al��t���n� hat�rlamam gerekiyor.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t64 = new GameText(64, "(d���n�yor...)", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t65 = new GameText(65, "�zerinde ki beyaz d��meye basarak �al��t�rabiliriz. Ancak bir kere kulland�ktan sonra bir ka� saniye beklemen gerekiyor.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t66 = new GameText(66, "Anlad�m fakat ne i�e yar�yor?", "Sen", WhosNext.Player, WhichStep.Step6);
        GameText t67 = new GameText(67, "Ah evet, ne i�e yarad���na gelirsek: Kameralara do�ru bu cihaz� konumland�r�p, beyaz d��meye bast���nda, kameray� yakla��k 10 saniye etkisiz hale getirebiliyorsun.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t68 = new GameText(68, "Waow! Cidden b�yle bir i�levi mi var? Bu �ok i�ime yarayabilir... Bilgiler i�in te�ekk�r ederim.", "Sen", WhosNext.Player, WhichStep.Step6);
        ToiletSpeakingTexts.Add(t62);
        ToiletSpeakingTexts.Add(t63);
        ToiletSpeakingTexts.Add(t64);
        ToiletSpeakingTexts.Add(t65);
        ToiletSpeakingTexts.Add(t66);
        ToiletSpeakingTexts.Add(t67);
        ToiletSpeakingTexts.Add(t68);
        // STEP 6

        // STEP 7
        GameText t69 = new GameText(69, "G�r�yorum ki labirenti ge�mi�sin. Ama bu katta da seni bekleyen �eyler var.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t70 = new GameText(70, "Ne gibi �eyler?", "Sen", WhosNext.Player, WhichStep.Step6);
        GameText t71 = new GameText(71, "Biraz al���lm���n d��� diyebiliriz. Arkada��n�n s�n�f�n� bulman gerekiyor. Orada sana yard�mc� olacak baz� �eyler olabilir.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        
        ToiletSpeakingTexts.Add(t69);
        ToiletSpeakingTexts.Add(t70);
        ToiletSpeakingTexts.Add(t71);
        // STEP 7

        // STEP 8 2. kat
        GameText t72 = new GameText(72, "Hah ho� geldin. Yava� yava� sona do�ru yakla��yorsun...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t73 = new GameText(73, "Evet. Sonunda bu okuldan kurtulaca��m!", "Sen", WhosNext.Player, WhichStep.Step6);
        GameText t74 = new GameText(74, "Senin ad�na seviniyorum.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t75 = new GameText(76, "Bu seninle son konu�mam�z. Di�er katta olamayaca��m maalesef.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t76 = new GameText(77, "Buraya kadar azimle geldin, seni tebrik ediyorum. Her neyse g�z ya�� d�kmeyi b�rakal�m.", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t77 = new GameText(78, "Di�er katlar�n aksine, bu katta okadar zorlanaca��n� d���nm�yorum. Bol �ans. V-Ve ho��akal...", "Konu�an Klozet", WhosNext.Closet, WhichStep.Step6);
        GameText t78 = new GameText(79, "Te�ekk�r ederim. Ho��akal...", "Sen", WhosNext.Player, WhichStep.Step6);

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
        GameText tc_1 = new GameText(2000, "Arkada��ndan Bir Not #1", "Burada neler oluyor hi� bir fikrim yok. Uyand���mda yan� ba��mda konu�an bir klozet vard�. Buradan hareket edemiyordu. Y�llard�r bu okulda oldu�unu s�yledi. Ama son zamanlarda bir �eyler de�i�mi�. Beni dikkatli olmam konusunda uyard�. Uyar�lar�n� dikkate alarak bilgi toplamak i�in yola koyuldum. Gizlice koridorlarda dolan�rken, siyah giyimli personellerin aralar�nda konu�malar�n� duyuyordum ve sesleri gittik�e yakla��yordu. Hemen saklanmam gerekiyordu. G�rd���m ilk kap�dan i�eriye atlad�m. Kafam� kald�rd���m ilk an, buran�n bir k�t�phane oldu�unu anlad�m. Bir not buldum. Notta bir pazarl�ktan bahsediyordu. ��k�� yolu i�in bulmam gereken anahtarlar oldu�unu ��rendim. Notun devam�n� okuyamadan saklanmak zorunda kald�m. Pazarl���n ne oldu�unu, neden bu yerde oldu�umu bilmiyorum. Korkuyorum! Buradan ka�mama asla izin vermeyecekler. Fakat bir yolunu bulaca��m...");
        GameText tc_2 = new GameText(2001, "Arkada��ndan Bir Not #2", "�lk anahtar� buldum. Di�erlerini bulmak bu kadar kolay olacakm� bilmiyorum. Anahtarlara dair hi� bir ipucu yok. Yan odada baz� sesler duydum. Klozetten gidip bir bilgi almam gerek galiba.");
        GameText tc_3 = new GameText(2002, "Arkada��ndan Bir Not #3", "Olamaz i�im �ok zor! Hangi sivrizekal� anahtar� ��renci dolab�na koymay� ak�l ederki? Neyse ki 2. koridorda ki dolaplardan birinde oldu�unu biliyorum. Aranmas� gereken �ok fazla dolap var. �st�ne�st�k kitliler. A�abilmem i�in bir alete ihtiyac�m var. Belki 3. koridorda dolan�rken g�rd���m depodan bir �eyler ��kar. Aramaya koyulmal�y�m.");
        GameText tc_4 = new GameText(2003, "Arkada��ndan Bir Not #4", "Hay�r olamaz! Bu katta hi� bir �ey bulamad�m. �stelik her yer g�venliklerle dolu. Bir an �nce alt kata gitmem gerekiyor.");
        GameText tc_5 = new GameText(2004, "Arkada��ndan Bir Not #5","Personelleri konu�urken duydum. �ansl�ysam bu katta 2 anahtar bulabilirim. Konu�an Klozetin bahsetti�i canl� kameralar�da g�rd�m. G�r�nmeden ge�ebilece�imi d���nd�m, onlar� hafife ald�m. Az kals�n yakalan�yordum. Bu depoya sakland�m. Bir alet g�rd�m. Biraz kurcalay�nca i�e yarar bir �ey oldu�unu anlad�m. Konu�an Klozetle bunu incelememiz ve ne i�e yarad���n� ��renmemiz gerek.");
        GameText tc_6 = new GameText(2005, "Arkada��ndan Bir Not #6", "��k��� bulmak i�in dola�t�m fakat oldu�um noktaya geri d�nd�m. Labirent gibi bir tasar�m� var. Personeller d�rt bir koldan dola��yor ve kritik noktalar� tutuyor. Acilen gizlenmem gerek...");
        GameText tc_7 = new GameText(2006, "Arkada��ndan Bir Not #7", "AMAN TANRIM! Bu okulda neler oluyor akl�m alm�yor. SINIFA G�RD���M ANDA BANA D�NEN O BAKI�LARDA NEY�N NES�YD�?!? Akl�m� ka��rmak �zereyim. Bu katta yemekhane oldu�unu ��rendim klozetten. Oray� hemen ara�t�r�p anahtar� bulmak zorunday�m. Orda olaca��na inan�yorum...");
        GameText tc_8 = new GameText(2007, "Arkada��ndan Bir Not #8", "Kattaki odalar� ara�t�rd�m. Ancak anahtar� bir t�rl� bulamad�m. Bir �ey farkettim. Her odadan 2 adet var ve k�t�phanelerden birini inceledi�imde bu kat�n krokisini g�rd�m...");
        GameText tc_9 = new GameText(2008, "Arkada��ndan Bir Not #9", "G�rd���m kadar�yla, baz� bilmecelerle kar�� kar��yay�m. Do�ru cevab�n beni ��k��a do�ru g�t�rece�ine inan�yorum.");
        GameText tc_10 = new GameText(2009, "Arkada��ndan Bir Not #10", "");


        
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
        GameText tc_1 = new GameText(2100, "G�venlik Anahtar�", "10. kat k�t�phanesinde bulunan g�venlik anahtar�.");
        GameText tc_2 = new GameText(2101, "G�venlik Anahtar�", "9. katta bulunan g�venlik anahtar�.");
        GameText tc_3 = new GameText(2102, "G�venlik Anahtar�", "8. katta bulunan g�venlik anahtar�.");
        GameText tc_4 = new GameText(2103, "G�venlik Anahtar�", "6. katta bulunan g�venlik anahtar�.");
        GameText tc_5 = new GameText(2104, "G�venlik Anahtar�", "6. katta bulunan g�venlik anahtar�.");
        GameText tc_6 = new GameText(2105, "G�venlik Anahtar�", "4. katta bulunan g�venlik anahtar�.");
        GameText tc_7 = new GameText(2106, "G�venlik Anahtar�", "3. katta bulunan g�venlik anahtar�.");
        GameText tc_8 = new GameText(2107, "G�venlik Anahtar�", "2. katta bulunan g�venlik anahtar�.");
        GameText tc_9 = new GameText(2108, "G�venlik Anahtar�", "2. katta bulunan g�venlik anahtar�.");
        GameText tc_10 = new GameText(2109, "G�venlik Anahtar�", "1. katta bulunan g�venlik anahtar�.");



        GameText tc_11 = new GameText(4000, "Personel odas� anahtar�", "Personel odas� anahtar�.");
        GameText tc_12 = new GameText(4001, "Yedek merdiven kap� anahtar�", "10. kattan ��k��� sa�lar.");

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
        GameText tc_1 = new GameText(3000, "B��ak", "Bir insan� ciddi �ekilde yaralayabilir.");
        GameText tc_2 = new GameText(3001, "Telsiz", "G�venlik kameras�n� iptal eder.");
        GameText tc_3 = new GameText(3002, "Crowbar", "Kilitli nesneleri a�abilir.");

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
        GameText ic_500 = new GameText(1000, "3 Ba�l� Mantar", "Zehirli olabilir.");
        GameText ic_501 = new GameText(1001, "Dev Ba�l� Mantar.", "Lezzetli bir mantar.");
        GameText ic_502 = new GameText(1002, "T�s Mantar�.", "�la�lar i�in kullan�l�r.");
        GameText ic_503 = new GameText(1005, "T�s Mantar�.", "�la�lar i�in kullan�l�r.");
        GameText ic_504 = new GameText(1008, "3 Mor Ba�l� Mantar", "Zehir yap�m�nda kullan�l�r.");

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
        GameText ic_500 = new GameText(1006, "Avc�l�k Kitab�", "Ay�lar nas�l avlan�r?");

        PuzzlesTexts.Add(ic_500);
    }
    private void PuzzleFlowerTextsInit()
    {
        GameText ic_500 = new GameText(1003, "Tekli Lavanta", "G�zel kokar.");
        GameText ic_501 = new GameText(1004, "�oklu Lavanta", "B�ceklerle �evrilidir.");
        GameText ic_502 = new GameText(1007, "Tekli G�l", "G�zelli�i ile �ne ��kar.");
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
            Debug.Log("Klozet konusma g�revi tamamlandi. ClosetSpeakedCount => " + InteractPanelController.instance.GetClosetSpeakedCount() + " MissionID => " + PuzzleManager.instance.MissionComplateController.GetToiletMissionID() + " Speaking Step => " + currentStep);
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