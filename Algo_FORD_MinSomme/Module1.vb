'Auteur : Philippe BORRIBO
'Version : 1.0
'Github : https://github.com/Philippe-Borribo
'LinkedIn : https://linkedin.com/in/philippe-borribo
'--- Bienvenu(e) dans notre programme de cheminement optimal avec l'alogorithme de Ford ---
'--- Ce programme consiste à déterminer le plus long chemin d'un tajet ---

Module Module1
    Structure arcs
        Dim successeur As Integer
        Dim predecesseur As Integer
        Dim ponderation As Double
    End Structure

    Sub Main()

        Dim nbrSommet, nbrArcs, depart, recommencer As Integer
        Dim temp As String
        recommencer = 1
        While recommencer
            Do
                Console.WriteLine("Entrer le nombre de sommet")
                temp = Console.ReadLine()
            Loop While (Not (IsNumeric(temp)) Or temp <= "1")
            nbrSommet = Convert.ToInt32(temp)

            Do
                Console.WriteLine("Entrer le nombre d'arcs du graphe ")
                temp = Console.ReadLine()
                If Not (IsNumeric(temp)) Then
                    temp = "0"
                End If
            Loop While (Convert.ToInt32(temp) < (nbrSommet - 1))
            nbrArcs = Convert.ToInt32(temp)

            Dim tabArcs = New arcs(nbrArcs) {}

            'Affichage des sommets du graphe'

            Console.Write("les sommets du graphe sont : ")
            For i As Integer = 0 To nbrSommet - 2
                Console.Write(i.ToString() + ", ")
            Next
            Console.Write((nbrSommet - 1).ToString())

            Console.WriteLine()


            'Saisie des valeurs pour chaque arc i'

            For i As Integer = 0 To nbrArcs - 1

                Do
                    Console.WriteLine("Entrer le successeur de l'arc u" + i.ToString)
                    temp = Console.ReadLine()
                    If Not (IsNumeric(temp)) Then
                        temp = "-1"
                    End If

                Loop While (Convert.ToInt32(temp) <= -1 Or Convert.ToInt32(temp) >= nbrSommet)

                tabArcs(i).successeur = Convert.ToInt32(temp)

                Do
                    Console.WriteLine("Entrer le predecesseur de l'arc u" + i.ToString)
                    temp = Console.ReadLine()
                    If Not (IsNumeric(temp)) Then
                        temp = "-1"
                    End If

                Loop While (tabArcs(i).successeur = Convert.ToInt32(temp) Or (Convert.ToInt32(temp) <= -1 Or Convert.ToInt32(temp) >= nbrSommet))

                tabArcs(i).predecesseur = Convert.ToInt32(temp)

                Do
                    Console.WriteLine("Entrer la ponderation de l'arc " + i.ToString)
                    temp = Console.ReadLine()
                Loop While (Not (IsNumeric(temp)))

                tabArcs(i).ponderation = Convert.ToDouble(temp)

            Next

            Do
                Console.WriteLine("Entrer le sommet de depart ")
                temp = Console.ReadLine()
            Loop While (Not (IsNumeric(temp)))

            depart = Convert.ToInt32(temp)


            Ford_PLC(tabArcs, nbrSommet, depart)
            Console.ReadKey()

            ' Recommencer le programme '
            Do
                Console.WriteLine("Voulez-vous recommencer ?")
                temp = Console.ReadLine()
            Loop While (temp <> "1" And temp <> "0")
            recommencer = Convert.ToInt32(temp)
            If Not (recommencer) Then
                Console.WriteLine("--- Merci d'avoir utilisé le programme ---")
            End If
        End While
        Console.ReadKey()

    End Sub

    Sub Ford_PLC(ByVal les_Arcs As arcs(), ByVal nbrSommet As Integer, ByVal depart As Integer)

        ' **** Algo de Ford pour le PLC (min somme)**** '

        ' Déclaration des variables et tableaux '
        Dim poids = New Double(nbrSommet) {}
        Dim predecesseur = New Integer(nbrSommet) {}
        Dim sommet = New Integer(nbrSommet) {}
        Dim sommetTraite = New Integer(nbrSommet) {}
        Dim position_i, position_j, k, l As Integer
        Dim modif As Boolean = False
        k = 0

        sommet(0) = depart
        For i As Integer = 1 To nbrSommet - 1
            If i <> depart Then
                sommet(i) = i
            Else : sommet(i) = 0

            End If
        Next

        ' Initialisation des poids et des predecesseurs de chaque sommet '

        poids(0) = 0 ' Le poids du sommet de depart '
        predecesseur(0) = depart
        For i As Integer = 1 To nbrSommet - 1
            poids(i) = Double.MinValue
            predecesseur(i) = i

        Next



        ' Iteration courante '

        For i As Integer = 0 To nbrSommet - 1
            l = selection(poids, sommet, sommetTraite)
            sommetTraite(i) = l
            position_i = recherchePosition(sommet, l)
            For j As Integer = 0 To les_Arcs.Length - 1
                If les_Arcs(j).predecesseur = l Then
                    position_j = recherchePosition(sommet, les_Arcs(j).successeur)
                    If poids(position_i) + les_Arcs(j).ponderation > poids(position_j) Then
                        poids(position_j) = poids(position_i) + les_Arcs(j).ponderation
                        predecesseur(position_j) = l
                        modif = True
                    End If
                End If
            Next

            If k = nbrSommet Then
                Console.WriteLine("--- !! Il existe un circuit qui prolonge à l'infini ---")
                Exit Sub
            End If

            If i = nbrSommet - 1 And modif = False Then
                Exit For
            ElseIf i = nbrSommet - 1 And modif = True Then
                i = -1
                'on recommence la boucle parce qu'il y a eu un(des) changement(s)
                k = k + 1
                For z As Integer = 0 To nbrSommet - 1
                    ' Réinitialisation des sommets traités pour le k suivant '
                    sommetTraite(z) = -1
                Next
                modif = False

            End If

        Next

        Console.WriteLine(k.ToString)
        Console.WriteLine("En partant du sommet " + depart.ToString + ", nous avons les poids et les predecesseurs suivants pour chaque sommet")
        For i As Integer = 0 To nbrSommet - 1
            Console.WriteLine(sommet(i).ToString + " : " + poids(i).ToString + " en provenace de " + predecesseur(i).ToString)
        Next

        Console.ReadKey()

    End Sub
    Function selection(ByVal poids As Double(), ByVal sommet As Integer(), ByVal sommetTraite As Integer())

        ' La fonction renvoie le sommet qui doit etre traité lors de l'iteration courante'

        ' Déclaration des variables '
        Dim max As Double = Double.MaxValue
        Dim present As Boolean = False
        Dim value As Integer

        For i As Integer = 0 To sommet.Length - 1
            For j As Integer = 0 To sommetTraite.Length - 1

                If sommet(i) = sommetTraite(j) Then
                    present = True
                End If

            Next
            If present = True Then
                present = False
                Continue For
            ElseIf poids(i) < max And poids(i) <> Double.MinValue Then
                max = poids(i)
                value = sommet(i)
            End If
        Next
        Return value

    End Function

    Function recherchePosition(ByVal tab As Integer(), ByVal k As Integer)

        ' La fonction renvoie la position d'un element dans un tableau'
        Dim pos As Integer = -1

        For i As Integer = 0 To tab.Length - 1
            If tab(i) = k Then
                pos = i
                Exit For
            End If
        Next

        Return pos

    End Function


End Module
