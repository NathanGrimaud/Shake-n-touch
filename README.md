# Shake-n-touch
Projet Windows Phone

##Note de version : 

 * Génération aléatoire de temps de shake : OK 
 * Affichage du temps restant : OK
 * Verification du shake pendant le temps restant : OK
 * Génération aléatoire de temps d'affichage du rectangle : OK
 * Affichage du rectangle : OK
 * Affichage des scores : OK 
 * Fin de partie : OK
 * Recommencer : NON
 * Page d'acceuil : NON

##Proposition d'amélioration

 * Faire changer le fond d'écran de couleur à chaque tick (voir le projet tetris) 
 * Faire des Levels (en fonction du score) 
 * Faire une page d'acceuil (les PSD seronts ajoutés)
 * Responsive Design 

## Commentaires 

 * N'oubliez pas dans le simulateurs d'utiliser la fonction "shake" dans données enregistrés. Sinon c'est quasi impossible de tester l'appli
 * Il y a une chose dans le .xaml.cs, nécessaire à la récupération des données de l'accéléromètre : <code> public static CoreDispatcher DispatcherAcceuil;
        public Acceuil()
        {
            this.InitializeComponent();
            DispatcherAcceuil = Dispatcher;
        }
</code>



