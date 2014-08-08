var geoMap=
{
	US:{code:"cn",noResults:"No Shortcut found. Try a full search of apple.com.",viewAll:"View all search results",searchText:"搜索"},		
	AU:{code:"au"},
	NZ:{code:"nz"},
	CA_EN:{code:"ca",directory:"/ca"},
	CA_FR:{code:"ca",directory:"/ca/fr",viewAll:"Afficher tous les résultats",searchText:"Recherche"},
	DE:{code:"de",viewAll:"Alle Suchergebnisse"},
	UK:{code:"uk"},
	FR:{code:"fr",viewAll:"Afficher tous les résultats"},
	CH_DE:{code:"ce",viewAll:"Alle Suchergebnisse"},
	CH_FR:{code:"cr",viewAll:"Afficher tous les résultats"},
	IE:null,
	JP:{code:"jp",noResults:"ショートカットは見つかりませんでした。検索はこちら。",viewAll:"すべての検索結果を見る"},
	IT:{code:"it",viewAll:"Mostra tutti i risultati"},
	ES:{code:"es",viewAll:"Ver todos los resultados de búsqueda"},
	NL:{code:"nl",viewAll:"Toon alle zoekresultaten"},
	BE_FR:{code:"bf",viewAll:"Afficher tous les résultats"},
	BE_NL:{code:"bl",viewAll:"Toon alle zoekresultaten"},
	TW:null,LA:{code:"la"},
	KR:{code:"kr",noResults:"일치하는 검색결과가 없습니다. 다시 검색하기.",viewAll:"검색 결과 전체 보기."},
	SE:{code:"se",noResults:"Ingen genväg hittades. Prova att fulltextsöka på apple.com.",viewAll:"Visa alla sökresultat"},
	DK:{code:"dk",noResults:"Ingen genvej fundet. Prøv at søge på hele apple.com.",
	viewAll:"Vis alle søgeresultater"},
	FI:{code:"fi",noResults:"Ei oikotietä. Etsi koko apple.com.",viewAll:"Katso hakutulokset"},
	NO:{code:"no",noResults:"Fant ingen snarvei. Søk på hele apple.com.",viewAll:"Vis alle søkeresultater"},
	BR:{code:"br"},ZA:{code:"za"},
	CN:{code:"cn",noResults:"没有搜索结果，请进行全站检索",viewAll:"查看所有记录",searchText:"搜索"},
	HK:null,
	RU:{code:"ru"},
	PT:null,
	PO:null,
	TR:null,
	UA:null,
	RO:null,
	CZ:null,
	HU:null,
	BG:null,
	HR:null,
	GR:null,
	IS:null
};
	
	var enhanceSearch=function(I)
	{
		var f=function(v)
		{
			var x=document.getElementById(v);
			x.parentNode.removeChild(x);
		};
		document.getElementById("g-search").setAttribute("action",I);
		document.getElementById("g-search").setAttribute("method","GET");
		f("search-oe");
		f("search-access");
		f("search-site");
		f("search-lr");
	};
	
	function loadShortcuts()
	{
		decorateGlobalSearchInput();
		if(typeof (searchCountry)=="undefined")
		{
			searchCountry="cn";
		}
		if(geoMap[searchCountry.toUpperCase()].directory)
		{
			var v=geoMap[searchCountry.toUpperCase()].directory;
		}
		else
		{
			if(searchCountry!="cn")
			{
				var v="/"+searchCountry.replace(/_/,"");
				//alert(v);
			}
			else
			{
				v="";
			}
		}
		var I=
		{
			"global":"http://www.apple.com.cn/search/index.php",
			"downloads":"http://www.apple.com.cn/search/search.htm",
			"iphone":"http://www.apple.com.cn/search/search.htm",
			"ipoditunes":"http://www.apple.com.cn/search/search.htm",
			"mac":"http://www.apple.com.cn/search/search.htm",
			"store":"http://www.apple.com.cn/search/search.htm",
			"support":"http://www.apple.com.cn/search/search.htm"
		};
		var f=I[searchSection]||"http://www.apple.com/search/";
		enhanceSearch(f);
		if((!navigator.userAgent.match(/iPhone/i))&&(typeof (deactivateSearchShortcuts)=="undefined"||!deactivateSearchShortcuts))
		{
			SearchShortcut.load();
		}
	}
		function shortcutsPageLoader(I)
		{
			var f=window.onload;
			if(typeof window.onload!="function")
			{
				window.onload=I;
			}
			else
			{
				window.onload=function(){f();I();};}
			}
			shortcutsPageLoader(loadShortcuts);
			var SearchShortcut=
			{
				baseUrl:"http://www.apple.com.cn/pro/js/shortcuts.php",
				minimumCharactersForSearch:0,
				entryDelay:150,
				currentRequest:false,
				descriptionCharacters:95,
				isIe:false,
				init:function()
				{
					this.fullSearchUrl=document.getElementById("globalsearch").getElementsByTagName("form")[0].getAttribute("action");
					this.noResults=geoMap["US"].noResults;
					this.viewAll=geoMap["US"].viewAll;
					if(typeof (searchCountry)!="undefined"&&searchCountry)
					{
						this.noResults=geoMap[searchCountry.toUpperCase()].noResults||this.noResults;
						this.viewAll=geoMap[searchCountry.toUpperCase()].viewAll||this.viewAll;
					}
					this.html=
					{
						results:document.getElementById("sp-results").getElementsByTagName("div")[0],
						input:document.getElementById("sp-searchtext")
					};
					if(navigator.userAgent.toLowerCase().indexOf("msie 6.")!=-1)
					{
						document.getElementById("sp-results").style.left="171px";
						this.isIe=true;}this.pausedControllers=[];
					},
					track:function(x,f)
					{
						if(typeof (s_gi)=="undefined"||!s_gi)
						{
							return ;
						}
						var v="appleglobal";
						var q="appleussearch";
						var I=null;
						if(typeof (searchCountry)!="undefined"&&searchCountry&&searchCountry!="US")
						{
							I=geoMap[searchCountry.toUpperCase()].code;
						}
						if(I)
						{
							v="apple"+I+"global";
							q="apple"+I+"search";
						}
						if(typeof (s_account)!="undefined"&&s_account.indexOf("appleussearch")==-1)
						{
							s=s_gi(s_account+","+q);
						}
						else
						{
							s=s_gi(v+","+q);
						}
						s.prop4="";
						s.g_prop4="";
						s.prop6="";
						s.g_prop6="";
						s.pageName="";
						s.g_pageName="";
						s.pageURL="";
						s.g_pageURL="";
						s.g_channel="";
						s.linkTrackVars="eVar2,eVar4,prop7,prop10";
						s.eVar2="WWW-sc: "+x.toLowerCase();
						s.prop7="WWW-sc: "+x.toLowerCase();
						s.eVar4=f;s.prop10=f;
						s.tl(this,"o","Shortcut Search");
					},
					go:function(f)
					{
						SearchShortcut.track(SearchShortcut.searchText,f);
						document.location=f;
					},
					search:function(v)
					{
						var f=this.baseUrl+"?q="+v;//encodeURIComponent(v);
						//alert(f);
						if(typeof (searchSection)!="undefined"&&searchSection)
						{
							f+="&section="+searchSection;
						}
						if(typeof (searchCountry)!="undefined"&&searchCountry)
						{
							f+="&geo="+searchCountry.toLowerCase();
						}
						this.spin();
						f+="&transport=js";
						var I=document.getElementsByTagName("head")[0];
						script=document.createElement("script");
						script.id="xdShortcutContainer";
						script.type="text/javascript";
						script.src=f;I.appendChild(script);
						SearchShortcut.scriptLoadTest();
					},
					scriptLoadTest:function()
					{
						var f=0;
						var I=window.setInterval(function(){f++;if(typeof (shortcutXml)!="undefined"){window.clearInterval(I);}else{if(f>20){window.clearInterval(I);document.getElementById("sp-search-spinner").style.display="none";}}},50);},loadXmlToDoc:function(I){var f;if(window.ActiveXObject){f=new ActiveXObject("Microsoft.XMLDOM");f.async="false";f.loadXML(I);}else{var v=new DOMParser();f=v.parseFromString(I,"text/xml");}if(!this.html||!this.html.results){this.init();}document.getElementById("sp-search-spinner").style.display="none";this.term=f.getElementsByTagName("term")[0].firstChild.nodeValue;this.xml=f.getElementsByTagName("search_results")[0];this.parseResults(this.xml);if(this.results){this.results.length>0?this.renderResults():this.renderNoResults();}},spin:function(){document.getElementById("sp-search-spinner").style.display="block";},parseResults:function(x){var v=x.getElementsByTagName("error");if(v.length>0){SearchShortcut.hideResults();return ;}else{var A=x.getElementsByTagName("match");this.results=new Array();for(var q=0;q<(A.length);q++){var f=A[q];var I={title:f.getAttribute("title"),url:f.getAttribute("url"),desc:f.getAttribute("copy"),category:f.getAttribute("category"),priority:f.getAttribute("priority"),image:f.getAttribute("image")};I.url=decodeURIComponent(I.url);this.results.push(I);}}},renderNoResults:function(){var x=this.noResults;this.html.results.innerHTML="";var I=document.createElement("ul");I.className="sp-results";listResult=document.createElement("li");listResult.className="firstCat resultCat";I.appendChild(listResult);listResult=document.createElement("li");listResult.id="sp-result-none";listResult.className="viewall";var f=document.createElement("div");f.className="hoverbox";var v=document.createElement("a");v.href=this.fullSearchUrl+"?q="+this.term;v.innerHTML=x;listResult.appendChild(f);listResult.appendChild(v);listResult.url=this.fullSearchUrl+"?q="+this.term;listResult.num=this.results.length;listResult.onclick=function(){SearchShortcut.go(this.url);};listResult.onmouseover=function(){SearchShortcut.itemSelected=true;};listResult.onmouseout=function(){SearchShortcut.itemSelected=false;};I.appendChild(listResult);this.html.results.appendChild(I);document.getElementById("globalsearch").className="active";},hideAllQuicktimeMovies:function(){if(typeof (AC)!="undefined"&&typeof (AC.Quicktime)!="undefined"&&typeof (AC.Quicktime.controllers)!="undefined"){function N(x){var i=curtop=0;if(x.offsetParent){i=x.offsetLeft;curtop=x.offsetTop;while(x=x.offsetParent){i+=x.offsetLeft;curtop+=x.offsetTop;}}return [i,curtop];}function w(S,j,Q,G,i,X,W,p){var b=S+Q;var d=j+G;var Z=i+W;var u=X+p;var g=Math.max(S,i);var Y=Math.max(j,X);var R=Math.min(b,Z);var x=Math.min(d,u);return R>g&&x>Y;}var f=AC.Quicktime.controllers;var r=$("sp-results");var T={width:328,height:448};var M=N(r);var A=M[0]-328;var q=M[1];var v=h+T.width;var I=m+T.height;for(var F=f.length-1;F>=0;F--){var o=f[F].movie;var L=Element.getDimensions(o);var O=N(o);var h=O[0];var m=O[1];if(w(h,m,L.width,L.height,A,q,T.width,T.height)){this.pausedControllers.push(f[F]);f[F].Stop();f[F].movie.style.visibility="hidden";}}}else{var J=document.getElementsByTagName("object");for(F=0;F<J.length;F++){if(typeof (J[F].Stop)!="undefined"){J[F].Stop();}try{if(typeof (J[F].getElementsByTagName("embed")[0].Stop)!="undefined"){J[F].getElementsByTagName("embed")[0].Stop();}}catch(k){}J[F].style.visibility="hidden";}}},showAllQuicktimeMovies:function(){if(typeof (AC)!="undefined"&&typeof (AC.Quicktime)!="undefined"&&typeof (AC.Quicktime.controllers)!="undefined"){for(var I=this.pausedControllers.length-1;I>=0;I--){this.pausedControllers[I].movie.style.visibility="visible";if(navigator.userAgent.match(/Firefox/i)){setTimeout(this.pausedControllers[I].Play,100);}else{this.pausedControllers[I].Play();}}this.pausedControllers=[];}else{var f=document.getElementsByTagName("object");for(I=0;I<f.length;I++){f[I].style.visibility="visible";if(typeof (f[I].Play)!="undefined"){f[I].Play();}try{if(typeof (f[I].getElementsByTagName("embed")[0].Play)!="undefined"){f[I].getElementsByTagName("embed")[0].Play();}}catch(v){}}}},startFlashFixTimer:function(){var I=0;var f=setInterval(function(){SearchShortcut.flashDomRender();I++;if(I>50){clearInterval(f);}},10);},border:5,flashDomFix:function(){document.getElementById("sp-results").firstChild.firstChild.style.border="5px none red";document.getElementById("globalsearch").onmousemove=function(){SearchShortcut.flashDomRender();};},flashDomRender:function(){SearchShortcut.border%2==0?SearchShortcut.border++:SearchShortcut.border--;var f=document.getElementById("sp-results").firstChild.firstChild;if(f){f.style.border=SearchShortcut.border+"px none red";}},itemSelected:false,renderResults:function(){this.html.results.innerHTML="";var o=document.createElement("ul");o.className="sp-results";var T={};for(var h=0;h<this.results.length;h++){var k=this.results[h];var w=unescape(k.desc);var q="";if(w.length>this.descriptionCharacters){w=w.substring(0,w.indexOf(" ",this.descriptionCharacters-11))+"&hellip;";q=unescape(k.desc);}var x=unescape(k.title);if(x.length>40){x=x.substring(0,x.indexOf(" ",30))+"&hellip;";}var m=document.createElement("li");m.id="sp-result-"+h;m.className="category-"+unescape(k.category).toLowerCase().replace(/\s+/g,"-");var v=document.createElement("div");v.className="hoverbox";var A=document.createElement("img");A.src=k.image;A.title=q;var M=document.createElement("span");M.className="text";var N=document.createElement("h4");var F=document.createElement("a");var I=document.createElement("p");F.href=decodeURIComponent(k.url);F.title=q;F.onclick=function(){SearchShortcut.go(decodeURIComponent(k.url));};F.innerHTML=x;I.innerHTML=w;I.title=q;N.appendChild(F);M.appendChild(N);M.appendChild(I);m.appendChild(v);m.appendChild(A);m.appendChild(M);m.url=k.url;m.num=h;m.onmouseover=function(){SearchShortcut.itemSelected=true;SearchShortcut.highlight(this);};m.onmouseup=function(){SearchShortcut.itemSelected=true;SearchShortcut.go(this.url);};m.onmouseout=function(){SearchShortcut.itemSelected=false;SearchShortcut.unhighlight(this);};m.priority=parseInt(k.priority);if(!T[k.category]){T[k.category]=new Array();}T[k.category].push(m);}var J="firstCat resultCat";for(var L in T){if(!T.hasOwnProperty(L)){continue;}m=document.createElement("li");m.className=J;m.innerHTML=unescape(L);J="resultCat";o.appendChild(m);for(var f=0;f<T[L].length;f++){o.appendChild(T[L][f]);}}m=document.createElement("li");m.id="sp-result-"+this.results.length;m.className="viewall";var v=document.createElement("div");v.className="hoverbox";var F=document.createElement("a");F.href=this.fullSearchUrl+"?q="+this.term;F.innerHTML=this.viewAll;m.appendChild(v);m.appendChild(F);m.url=this.fullSearchUrl+"?q="+this.term;m.num=this.results.length;m.onclick=function(){SearchShortcut.go(this.url);};m.onmouseover=function(){SearchShortcut.itemSelected=true;};m.onmouseout=function(){SearchShortcut.itemSelected=false;};document.getElementById("globalsearch").className="active";o.appendChild(m);this.html.results.appendChild(o);this.hideAllQuicktimeMovies();if(typeof (flashOnPage)!="undefined"&&flashOnPage){this.flashDomFix();this.startFlashFixTimer();}},startKeystrokeTimer:function(){if(this.timeoutId){window.clearTimeout(this.timeoutId);}this.timeoutId=window.setTimeout("SearchShortcut.commitKeystroke()",this.entryDelay);},commitKeystroke:function(){this.search(this.searchText);},hideResults:function(f,I){if(!this.html){this.init();}this.selected=null;document.getElementById("globalsearch").className="";this.html.results.innerHTML="";this.showAllQuicktimeMovies();},highlight:function(f){f.className="hoverli";},keyHighlight:function(f){if(this.selected){this.selected.className="";}this.selected=f;f.className="hoverli";},unhighlight:function(f){f.className="";},load:function(){var f=document.createElement("img");f.src="http://images.apple.com/global/nav/images/spinner.gif";f.width="11";f.height="11";f.border="0";f.alt="*";f.id="sp-search-spinner";f.style.display="none";document.getElementById("globalsearch").appendChild(f);document.getElementById("g-search").onsubmit=function(I){return false;};if(navigator.userAgent.match(/AppleWebKit/i)){document.getElementById("sp-searchtext").onkeydown=function(I){var v=typeof (event)!="undefined"?event["keyCode"]:I.keyCode;if(!I){I=event;}if(v==13&&!I.altKey){if(I.target.value.length===0){return false;}if(SearchShortcut.selected){SearchShortcut.go(SearchShortcut.selected.url);}else{SearchShortcut.hideResults();document.getElementById("g-search").submit();}}};}document.getElementById("sp-searchtext").onkeyup=function(I){var x=typeof (event)!="undefined"?event["keyCode"]:I.keyCode;if(!I){I=event;}if(x==40&&SearchShortcut.results){try{I.preventDefault();I.stopPropagation();}catch(A){}if(SearchShortcut.selected&&(SearchShortcut.results.length>SearchShortcut.selected.num+1)){SearchShortcut.keyHighlight(document.getElementById("sp-result-"+(SearchShortcut.selected.num+1)));}if(!SearchShortcut.selected&&SearchShortcut.results.length>0){SearchShortcut.keyHighlight(document.getElementById("sp-result-0"));}SearchShortcut.flashDomRender();}else{if(x==38&&SearchShortcut.results){try{I.preventDefault();I.stopPropagation();}catch(A){}if(SearchShortcut.selected&&SearchShortcut.selected.num>0){SearchShortcut.keyHighlight(document.getElementById("sp-result-"+(SearchShortcut.selected.num-1)));}SearchShortcut.flashDomRender();}else{if(x==27){SearchShortcut.hideResults();document.getElementById("sp-searchtext").value="";}else{SearchShortcut.selected=false;var v=document.getElementById("sp-searchtext").value;v=v.replace(/[%\^\?\!\*\/<>\$]/ig,"");v=v.replace(/^\s+/g,"").replace(/\s+$/g,"");if(v.length<1&&SearchShortcut.html){SearchShortcut.html.results.innerHTML="";document.getElementById("sp-search-spinner").style.display="none";SearchShortcut.hideResults();}else{if(v.length>SearchShortcut.minimumCharactersForSearch){SearchShortcut.searchText=v;SearchShortcut.startKeystrokeTimer();}}}}}};}};function decorateGlobalSearchInput(){var J=document.getElementById("sp-searchtext");var q=null;var x=0;var i="Search";if(typeof (searchCountry)=="undefined"){searchCountry="us";}if(geoMap[searchCountry.toUpperCase()].searchText){i=geoMap[searchCountry.toUpperCase()].searchText;}var h="";if(navigator.userAgent.match(/AppleWebKit/i)){J.setAttribute("type","search");if(!J.getAttribute("results")){J.setAttribute("results",x);}if(null!=i){J.setAttribute("placeholder",i);J.setAttribute("autosave",h);}J.onblur=function(){if(!SearchShortcut.itemSelected){SearchShortcut.hideResults();}};}else{J.setAttribute("autocomplete","off");q=document.createElement("input");J.parentNode.replaceChild(q,J);var I=document.createElement("span");I.className="left";var N=document.createElement("span");N.className="right";var m=document.createElement("div");m.className="reset";var f=document.createElement("div");f.className="search-wrapper";var A=J.value==i;var v=J.value.length==0;if(A||v){J.value=i;f.className+=" blurred empty";}f.appendChild(I);f.appendChild(J);f.appendChild(N);f.appendChild(m);J.onfocus=function(){var T=f.className.indexOf("blurred")>-1;if(J.value==i&&T){J.value="";}f.className=f.className.replace("blurred","");};J.onblur=function(){if(!SearchShortcut.itemSelected){SearchShortcut.hideResults();}if(J.value==""){f.className+=" empty";J.value=i;}f.className+=" blurred";};J.onkeydown=function(T){var F=typeof (event)!="undefined"?event["keyCode"]:T.keyCode;if(!T){T=event;}if(F==13&&!T.altKey){var L=null;if(T.target){L=T.target;}else{if(T.srcElement){L=T.srcElement;}}if(L.value.length===0){return false;}if(SearchShortcut.selected){SearchShortcut.go(SearchShortcut.selected.url);}else{SearchShortcut.hideResults();document.getElementById("g-search").submit();}return ;}if(J.value.length>=0){f.className=f.className.replace("empty","");}o();};var o=function(){return (function(T){var L=false;if(!T){T=window.event;}if(T.type=="keydown"){if(T.keyCode!=27){return ;}else{L=true;}}J.blur();J.value="";f.className+=" empty";J.focus();});};m.onmousedown=o();if(q){q.parentNode.replaceChild(f,q);}}}
