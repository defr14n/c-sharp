            CryptorEngine cr = new CryptorEngine();
            IniFile ini = new IniFile("F:\\share\\database.ini");
           // ---------------string server=, port = "3307", uid="root", pwd="", database="rpl_perpustakaan";

            // enkripsp
           // String server = cr.Encrypt("localhost", true);
           //String port= cr.Encrypt("3306" , true);
           // String uid = cr.Encrypt("root" , true);
           // String pwd = cr.Encrypt("" , true);
           // String database = cr.Encrypt("rpl_perpustakaan" , true);

           // //menulis ke file
           // ini.IniWriteValue("DATABASE", "server", server);
           // ini.IniWriteValue("DATABASE", "port", port );
           // ini.IniWriteValue("DATABASE", "uid", uid );
           // ini.IniWriteValue("DATABASE", "pwd", pwd );
           // ini.IniWriteValue("DATABASE", "database", database );
            //membaca yang terenkripsi
            String server = cr.Decrypt( ini.IniReadValue("database", "server"),true);
            String port = cr.Decrypt( ini.IniReadValue("database", "port"),true);
            String uid = cr.Decrypt (ini.IniReadValue("database", "uid"),true);
            String pwd = cr.Decrypt(ini.IniReadValue("database", "pwd"),true);
            String database = cr.Decrypt(ini.IniReadValue("database", "database"), true);
            Console.WriteLine(server);
            Console.WriteLine(port);
            Console.WriteLine(uid);
            Console.WriteLine(pwd);
            Console.WriteLine(database );
            Console.ReadKey();
