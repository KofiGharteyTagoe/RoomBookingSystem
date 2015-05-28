<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllForms.aspx.cs" Inherits="LoginAppmain.AllForms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HOME PAGE</title>

    <link href="AllFormsSheet.css" rel="stylesheet" />
        <script src="AllFormsJs.js"></script>

</head>
<body>
    <form id="NormaLayout" runat="server">

        <div class="header">
            <div class="mainhead">
                <table class="headertable">
                    <tr>
                        <td class="headertd">
                    <h3 class="welcome"> Welcome: <asp:Label ID="lbl_fullname" runat="server" Text="Fullname goes here"></asp:Label></h3><!-- Full name of user Goes here -->
                        </td>
                        <td class="headertd">
                            <asp:LinkButton ID="Logout" runat="server" OnClick="btn_logout_Click" Text="Logout">Logout</asp:LinkButton>

                        </td>
                    </tr>
                </table>

                <asp:Image ID="Image1" runat="server" Height="53px" ImageUrl="~/logo.png" />
                <hr />
            </div>

            <div class="container">
                            </div>
                <div class="logo">
                    <h1 class="headercolour">ROOM BOOKING </h1>
                </div>

                <div class="navigate">  
                    <ul>
                        <li><a href="Calendar.aspx">Calendar</a> </li>
                        <li id="Modify Rooms"><a href="RoomsResources.aspx">Rooms</a> </li>
                        <li id="Users"><a href="#">Manage Users</a>
                      <ul>   
                             <li><a href="SearchUser.aspx">Search Users</a> </li>
                              <li><a href="ModifyUser.aspx">Modify Users</a> </li>
                      </ul>
                        </li>
                        <li><a href="#">My Bookings</a> 
                           <ul>   
                             <li><a href="Modify Booking.aspx">Modify Booking</a> </li>
                              <li><a href="AddBooking.aspx">Make A Booking</a> </li>
                            </ul>
                        </li>
                        <li><a href="My Account.aspx">My Account</a> </li>
                    </ul>
                </div>

        </div>
        
            <div class="content">
                            <div class="Pagename">

                HOME PAGE
            </div>
                
        <div class="container">

            



                <p class="Para">The ease or difficulty of deciphering a word depends on the language. Dictionaries categorize a language's lexicon (i.e., its vocabulary) into lemmas. These can be taken as an indication of what constitutes a "word" in the opinion of the writers of that language.

Semantic definition[edit]
Leonard Bloomfield introduced the concept of "Minimal Free Forms" in 1926. Words are thought of as the smallest meaningful unit of speech that can stand by themselves.[1] This correlates phonemes (units of sound) to lexemes (units of meaning). However, some written words are not minimal free forms as they make no sense by themselves (for example, the and of).[2]

Some semanticists have put forward a theory of so-called semantic primitives or semantic primes, indefinable words representing fundamental concepts that are intuitively meaningful. According to this theory, semantic primes serve as the basis for describing the meaning, without circularity, of other words and their associated conceptual denotations.[3]

Features[edit]
In the Minimalist school of theoretical syntax, words (also called lexical items in the literature) are construed as "bundles" of linguistic features that are united into a structure with form and meaning.[4] For example, the word "bears" has semantic features (it denotes real-world objects, bears), category features (it is a noun), number features (it is plural and must agree with verbs, pronouns, and demonstratives in its domain), phonological features (it is pronounced a certain way), etc.

Word boundaries[edit]
The task of defining what constitutes a "word" involves determining where one word ends and another word begins—in other words, identifying word boundaries. There are several ways to determine where the word boundaries of spoken language should be placed:

Potential pause: A speaker is told to repeat a given sentence slowly, allowing for pauses. The speaker will tend to insert pauses at the word boundaries. However, this method is not foolproof: the speaker could easily break up polysyllabic words, or fail to separate two or more closely related words.
Indivisibility: A speaker is told to say a sentence out loud, and then is told to say the sentence again with extra words added to it. Thus, I have lived in this village for ten years might become My family and I have lived in this little village for about ten or so years. These extra words will tend to be added in the word boundaries of the original sentence. However, some languages have infixes, which are put inside a word. Similarly, some have separable affixes; in the German sentence "Ich komme gut zu Hause an", the verb ankommen is separated.
Phonetic boundaries: Some languages have particular rules of pronunciation that make it easy to spot where a word boundary should be. For example, in a language that regularly stresses the last syllable of a word, a word boundary is likely to fall after each stressed syllable. Another example can be seen in a language that has vowel harmony (like Turkish):[5] the vowels within a given word share the same quality, so a word boundary is likely to occur whenever the vowel quality changes. Nevertheless, not all languages have such convenient phonetic rules, and even those that do present the occasional exceptions.
Orthographic boundaries: See below.
Orthography[edit]
In languages with a literary tradition, there is interrelation between orthography and the question of what is considered a single word. Word separators (typically spaces) are common in modern orthography of languages using alphabetic scripts, but these are (excepting isolated precedents) a relatively modern development (see also history of writing).

In English orthography, compound expressions may contain spaces. For example, ice cream, air raid shelter and get up each are generally considered to consist of more than one word (as each of the components are free forms, with the possible exception of get).

Not all languages delimit words expressly. Mandarin Chinese is a very analytic language (with few inflectional affixes), making it unnecessary to delimit words orthographically. However, there are a great number of multiple morpheme compounds in Chinese in addition to a variety of bound morphemes that make it difficult to clearly determine what constitutes a word.

Sometimes, languages which are extremely close grammatically will consider the same order of words in different ways. For example, reflexive verbs in the French infinitive are separate from their respective particle – e.g. se laver ("to wash oneself"), in Portuguese they are hyphenated – lavar-se, while in Spanish they are joined – lavarse.[6]

Japanese uses orthographic cues to delimit words such as switching between kanji (Chinese characters) and the two kana syllabaries. This is a fairly soft rule, as content words can also be written in hiragana for effect (though if done extensively spaces are typically added to maintain legibility).

Vietnamese orthography, although using the Latin alphabet, delimits monosyllabic morphemes rather than words.

In character encoding, word segmentation depends on which characters are defined as word dividers.

Morphology[edit]
Main article: Morphology (linguistics)
Further information: Inflection

Letters and words
In synthetic languages, a single word stem (for example, love) may have a number of different forms (for example, loves, loving, and loved). However for some purposes these are not usually considered to be different words, but rather different forms of the same word. In these languages, words may be considered to be constructed from a number of morphemes. In Indo-European languages in particular, the morphemes distinguished are

the root
optional suffixes
a desinence, or inflectional suffix.
Thus, the Proto-Indo-European *wr̥dhom would be analyzed as consisting of

*wr̥-, the zero grade of the root *wer-
a root-extension *-dh- (diachronically a suffix), resulting in a complex root *wr̥dh-
The thematic suffix *-o-</p>
            </div>
        </div>
    </form>
    <p>
        &nbsp;
    </p>
</body>
</html>
