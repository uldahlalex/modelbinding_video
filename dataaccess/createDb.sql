CREATE TABLE patients (
                          id INTEGER PRIMARY KEY AUTOINCREMENT,
                          name TEXT NOT NULL
);

CREATE TABLE diseases (
                          id INTEGER PRIMARY KEY AUTOINCREMENT,
                          name TEXT NOT NULL
);

CREATE TABLE diagnoses (
                           id INTEGER PRIMARY KEY AUTOINCREMENT,
                           patient_id INTEGER NOT NULL,
                           disease_id INTEGER NOT NULL,
                           diagnosis_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                           FOREIGN KEY (patient_id) REFERENCES patients(id) ON DELETE CASCADE,
                           FOREIGN KEY (disease_id) REFERENCES diseases(id) ON DELETE CASCADE
);

-- Insert sample data
INSERT INTO patients (name) VALUES
                                ('Peter'),
                                ('Bob');

INSERT INTO diseases (name) VALUES
                                ('Flu'),
                                ('Diarrhea');

INSERT INTO diagnoses (patient_id, disease_id) VALUES
                                                   (1, 1),
                                                   (2, 2);